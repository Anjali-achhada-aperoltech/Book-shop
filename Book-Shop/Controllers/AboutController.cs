using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
