using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService service)
        {
            cartService = service;
        }
        [Authorize]
        public async Task<IActionResult> AddCart(CartDto c1, Guid Id)
        {
            c1.BookitemId = Id;
            var data = await cartService.InsertAsync(c1, Id);

            if (data != null && data.Id != Guid.Empty) 
            {
                return Json(new { success = true, message = "Product added to cart successfully!" });
            }

            return Json(new { success = false, message = "Failed to add product to cart." });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItemInCart([FromBody] CartDto c1)
        {
            if (c1 == null || c1.Id == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid product ID." });
            }

            c1.quantity = 1; // Ensure quantity starts at 1
            var data = await cartService.InsertAsync(c1, c1.Id);

            if (data != null && data.Id != Guid.Empty)
            {
                return Json(new { success = true, message = "Product added to cart successfully!" });
            }

            return Json(new { success = false, message = "Failed to add product to cart." });
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var data = await cartService.GetallAsync();
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(Guid id)
        {
            var removecartlist = await cartService.DeleteAsync(id);
            if (removecartlist)
            {
                return Json(new { success = true, message = "CartList item deleted successfully." });
            }
            return Json(new { success = false, message = "Failed to delete CartList item." });


        }
        public async Task<IActionResult> plus(Guid id)
        {
            var data = await cartService.IncrementCartItem(id);
            return RedirectToAction("Index", "cart");
        }
        public async Task<IActionResult> minus(Guid id)
        {
            var data = await cartService.DecreMentItem(id);
            return RedirectToAction("Index", "cart");
        }
        public async Task<IActionResult> GetData()
        {
            var cartdetails = cartService.GetQuantity();
            var count = cartdetails.Result;

            return Json(new { count = count });

        }
    }
}


