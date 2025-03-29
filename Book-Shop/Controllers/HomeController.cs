using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index(Guid? categoryId, string searchItems, int page = 1, int limit = 6)
        {
            var categoryList = await SeletCategoryList();
            ViewBag.categorydata = categoryList.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            // Get all items based on category
            var result = await _service.GetAllCategoryAsync(categoryId);
            if (result == null)
            {
                return NotFound();
            }

            // Apply search filter
            if (!string.IsNullOrEmpty(searchItems))
            {
                searchItems = searchItems.Trim();
                result = result.Where(x => x.Name.Contains(searchItems, StringComparison.OrdinalIgnoreCase)
                                        || x.CategoryName.Contains(searchItems, StringComparison.OrdinalIgnoreCase))
                               .ToList();
            }

            // Pagination Logic
            int totalItems = result.Count();
            var paginatedResult = result.Skip((page - 1) * limit).Take(limit).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / limit);
            ViewBag.SearchItems = searchItems;

            return View(paginatedResult);
        }

        public async Task<List<CategoryDto>> SeletCategoryList()
        {
            var result = await _service.SelctCategoryList();
            return result;

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
