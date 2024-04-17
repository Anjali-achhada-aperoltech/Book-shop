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
        public async Task<IActionResult> AddCart(CartDto c1,Guid Id)
        {
            c1.BookitemId =Id;
            var data=await cartService.InsertAsync(c1, Id);
            return View();
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
        public async Task<IActionResult>minus(int quantity,Guid id)
        {
            var data = await cartService.DecreMentItem(quantity,id);
            return RedirectToAction("Index");
        }

    }
    }


