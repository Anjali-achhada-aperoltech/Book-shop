using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Controllers
{
    public class productController : Controller
    {
        public readonly IBookItemService _service;

        public productController(IBookItemService service)
        {
            _service = service;

        }
      
            public async Task<IActionResult> GetAll(string Searchitems)
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
        }
    }

