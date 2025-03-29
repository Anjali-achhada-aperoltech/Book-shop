using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;

namespace Book_Shop.Controllers
{
    public class ViewCategoryController : Controller
    {
        private readonly IBookItemService bookItemService;
        public ViewCategoryController(IBookItemService bookItemService)
        {
            this.bookItemService = bookItemService;
        }
        public async Task<IActionResult> Index(Guid? categoryId, string searchItems, int page = 1, int limit = 6)
        {
            

            // Get all items based on category
            var result = await bookItemService.GetAllCategoryAsync(categoryId);
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
    }
}
