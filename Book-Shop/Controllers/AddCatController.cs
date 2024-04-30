using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Shop.Controllers
{
    public class AddCatController : Controller
    {
        private readonly ICartService _cartService;
        public AddCatController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data =await  _cartService.GetallAsync();
            if (data == null)
            {
                return NotFound();
            }
            ViewBag.categories = new SelectList((System.Collections.IEnumerable)data, "Id", "Name");
            return View(data);
        }
    }
}
