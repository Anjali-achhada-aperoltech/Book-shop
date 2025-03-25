using Book.Interfaces.Services;
using Book.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Book_Shop.Controllers
{
    public class WishListController : Controller
    {
        private readonly IwishListService _wishlistservice;
        public WishListController(IwishListService wishlistservice)
        {
            _wishlistservice = wishlistservice;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddWishList(Guid BookitemId)
        {
            if (BookitemId == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid product." });
            }

            bool result = await _wishlistservice.AddToWishlist(BookitemId);

            if (result)
            {
                return Json(new { success = true, message = "Item added to WishLIst!" });
            }
            else
            {
                return Json(new { success = false, message = "Product is already in wishlist!" });
            }
        }
        public async Task<IActionResult> GetWishlistCount()
        {
            var count = await _wishlistservice.GetWishlistCount();
            return Json(new { count = count });
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var getallwishlist = await _wishlistservice.GetAllWishList();
            if (getallwishlist == null)
            {
                return NotFound();
            }
            return View(getallwishlist);
        }
        public async Task<IActionResult>DeleteWishList(Guid Id)
        {
            var removeWishList = await _wishlistservice.RemoveFromWishlist(Id);
            if (removeWishList)
            {
                return Json(new { success = true, message = "Wishlist item deleted successfully." });
            }
            return Json(new { success = false, message = "Failed to delete wishlist item." });


        }


    }
}
