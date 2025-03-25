using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ROMS.Services;


namespace Book.Services
{
    public class wishListService : ServiceBase, IwishListService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<Applicationuser> userManager;

        public wishListService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, UserManager<Applicationuser> userManager) : base(unitOfWork)
        {
            _contextAccessor = contextAccessor;
            this.userManager = userManager;
        }
        public async Task<WishListDTO> GetAllWishList()
        {
            var user = _contextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = userManager.GetUserId(user);
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            // Fetch wishlist items for the user
            var wishListItems = await unitOfWork.wishlistRepositiory.FindByAsync(
                x => !x.IsDeleted && x.ApplicationuserId == userId
, includeProperties: "BookItem"); // Ensure it's never null

            var wishlistVm = new WishListDTO
            {
                WishlistItems = wishListItems
                    .Where(w => w.BookItem != null) // Ensure BookItem is not null
                    .Select(w => new WishListItemDTO
                    {
                        Id=w.Id,
                        BookId = w.BookItem.Id,
                        BookName = w.BookItem.Name,
                        Price = w.BookItem.price,
                        BookImage = w.BookItem.FrontImage
                    }).ToList()
            };

            return wishlistVm;
        }

        public async Task<int> GetWishlistCount()
        {
            var user = _contextAccessor.HttpContext.User;
            var userId = userManager.GetUserId(user);

            if (string.IsNullOrEmpty(userId))
            {
                return 0; 
            }

            var wishlistItems = await unitOfWork.wishlistRepositiory.FindByAsync(x => x.ApplicationuserId == userId && !x.IsDeleted);
            return wishlistItems.Count(); 
        }
        public async Task<bool> DeleteAsync(Guid id,bool isharddelete=false)
        {
            var data = await unitOfWork.wishlistRepositiory.GetAsync(id);
            if (data != null && isharddelete)
            {
                var val = await unitOfWork.wishlistRepositiory.DeleteAsync(data,true);
                return true;
            }

            else
            {
                return false;
            }
        }


        public async Task<bool> AddToWishlist(Guid bookId)
        {
            var user = _contextAccessor.HttpContext.User;
            var userId = userManager.GetUserId(user);

            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User is not authenticated");

            var userExists = await userManager.FindByIdAsync(userId);
            if (userExists == null)
                throw new InvalidOperationException("User does not exist");

            var exists = await unitOfWork.wishlistRepositiory.FindSingleByAsync(w => w.ApplicationuserId == userId && w.BookitemId == bookId && !w.IsDeleted);

            if (exists != null)
                return false; 

            var wishlist = new WishList
            {
                Id = Guid.NewGuid(),
                ApplicationuserId = userId,
                BookitemId = bookId
            };

            await unitOfWork.wishlistRepositiory.AddAsync(wishlist);
            return true;
        }

        public async Task<bool> RemoveFromWishlist(Guid bookId)
        {
            var user = _contextAccessor.HttpContext.User;
            var userId = userManager.GetUserId(user);

            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User is not authenticated");

            var wishlistItem = await unitOfWork.wishlistRepositiory.FindSingleByAsync(w => w.ApplicationuserId == userId && w.Id == bookId);

            if (wishlistItem == null)
                return false;
            wishlistItem.IsDeleted = true;
           await unitOfWork.wishlistRepositiory.DeleteAsync(wishlistItem);
            return true;
        }
    }
}
