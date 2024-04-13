using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
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

    }
    }

