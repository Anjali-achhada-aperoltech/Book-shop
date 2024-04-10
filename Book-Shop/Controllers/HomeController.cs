using Book.Interfaces.Services;
using Book_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Shop.Controllers
{
    public class HomeController : Controller
    {
        public readonly IBookItemService _service;
        public HomeController(IBookItemService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index()
        {
            var data =await  _service.GetAllAsync();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return View(data);
            }
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return View(data);

            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
