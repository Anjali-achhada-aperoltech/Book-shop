using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Controllers
{
    public class MenuItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
