using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Book_Shop.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutUsService _aboutUsService;
        public AboutController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        public async  Task<IActionResult> Index()
        {
            var data = await _aboutUsService.GetAllAsync();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return View(data);
            }
        }
    }
}
