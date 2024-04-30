using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService service)
        {
            cartService= service;
        }
        [Authorize]
        public async Task<IActionResult> AddCart(CartDto c1,Guid Id)
        {
            c1.BookitemId =Id;
            var data=await cartService.InsertAsync(c1, Id);

            if (data != null && data.Id != Guid.Empty) // assuming an empty Guid indicates failure
            {
                return Json(new { success = true, message = "Product added to cart successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to add product to cart." });
            }
        }
        [Authorize]
       public async Task<IActionResult> Index()
        {
            var data =await  cartService.GetallAsync();
            if (data == null)
            {
                return NotFound();
            }
            
            return View(data);
        }
        [HttpPost]
    
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await cartService.DeleteAsync(id);
            TempData["delete"] = "Delete data successfully";
            return RedirectToAction("Index", "Cart");
        }
        public async Task<IActionResult>plus(Guid id)
        {
            var data=await cartService.IncrementCartItem(id);
            return RedirectToAction("Index","cart");
        }
        public async Task<IActionResult>minus(Guid id)
        {
            var data = await cartService.DecreMentItem(id);
            return RedirectToAction("Index","cart");
        }
        public async Task<IActionResult>GetData(Guid userId)
        {
            var data = cartService.GetQuantity(userId);
            return Ok(data);
        }

    }
    }


