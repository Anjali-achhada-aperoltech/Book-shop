using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.ProjectCache;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> Index(string Searchitems, int limit = 6)
        {
            var result = await _service.GetAllAsync();
            if (result == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(Searchitems))
            {
                Searchitems = Searchitems.Trim();
                result = result.Where(x => x.Name.Contains(Searchitems, StringComparison.OrdinalIgnoreCase)
                                        || x.CategoryName.Contains(Searchitems, StringComparison.OrdinalIgnoreCase))
                               .ToList();
            }



            return View(result);

        }


       
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
           
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                return NotFound();
            }
           
            return View(data);
        
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
