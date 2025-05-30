﻿using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ROMS.Services;

namespace Book.Services
{
    public class CartService : ServiceBase, ICartService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<Applicationuser> userManager;
        public CartService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<Applicationuser> userManager) : base(unitOfWork)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        public async Task<bool> DecreMentItem(Guid id)
        {
            var cartItem = await unitOfWork.cartReposititory.FindSingleByAsync(x => x.Id == id);
            if (cartItem == null)
            {
                return false;
            }
            if (cartItem.quantity > 1)
            {
                cartItem.quantity -= 1;
                await unitOfWork.cartReposititory.UpdateAsync(cartItem);
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = await unitOfWork.cartReposititory.GetAsync(id);
            if (data != null && id != null)
            {
                data.IsDeleted = true;
                var val = await unitOfWork.cartReposititory.DeleteAsync(data);
                return true;
            }

            else
            {
                return false;
            }
        }
        public async Task<CartVm> GetallAsync()
        {
            var user = httpContextAccessor.HttpContext.User;
            var data = userManager.GetUserId(user);
            CartVm v1 = new CartVm()
            {
                Carts = await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == data && !x.IsDeleted, includeProperties: "BookItem")
            };
            foreach (var item in v1.Carts)
            {
                v1.Total += (double)(item.BookItem.price * item.quantity);
            }
            return v1;
        }
        public async Task<bool> IncrementCartItem(Guid id)
        {
            var cartItem = await unitOfWork.cartReposititory.FindSingleByAsync(x => x.Id == id);
            if (cartItem == null)
            {
                return false;
            }

            cartItem.quantity += 1;
            await unitOfWork.cartReposititory.UpdateAsync(cartItem);
            return true;
        }

        public async Task<CartDto> InsertAsync(CartDto cart, Guid Id)
        {
            try
            {
                var bookItemExists = await unitOfWork.bookItemsRepositiory.FindByAsync(b => b.Id == Id);

                cart.Id = Guid.NewGuid();
                cart.BookitemId = Id;
                var user = httpContextAccessor.HttpContext.User;
                var data = userManager.GetUserId(user);
                var relatedData = await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == data);
                if (string.IsNullOrEmpty(data))
                    throw new InvalidOperationException("User is not authenticated");
                var userExists = await userManager.FindByIdAsync(data);
                if (userExists == null)
                    throw new InvalidOperationException("User does not exist");
                var existingCart = await unitOfWork.cartReposititory.FindSingleByAsync(
                   c => c.ApplicationuserId == data && c.BookitemId == cart.BookitemId && !c.IsDeleted);

                if (existingCart != null)
                {
                    existingCart.quantity += cart.quantity;
                    //cart.quantity = IncreMenttItem(cart, cart.quantity);
                }
                else
                {
                    Cart newCart = new Cart
                    {
                        ApplicationuserId = data,
                        BookitemId = Id,
                        quantity = cart.quantity
                    };

                    await unitOfWork.cartReposititory.AddAsync(newCart);
                }
                var existingWishlistItem = await unitOfWork.wishlistRepositiory.FindSingleByAsync(
            w => w.ApplicationuserId == data && w.BookitemId == Id && !w.IsDeleted);

                if (existingWishlistItem != null)
                {
                    await unitOfWork.wishlistRepositiory.DeleteAsync(existingWishlistItem, isHardDelete: true);

                }

                await unitOfWork.cartReposititory.SaveAsync();
                await unitOfWork.wishlistRepositiory.SaveAsync();

                return cart;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public async Task<int> GetQuantity()
        {
            var cartItems = await unitOfWork.cartReposititory.FindByAsync(x => !x.IsDeleted);

            int count = cartItems.Select(c => c.BookitemId).Distinct().Count();

            return count > 0 ? count : 0; // Ensure 0 if no items
        }



    }
}






