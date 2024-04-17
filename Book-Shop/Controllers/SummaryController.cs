using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Controllers
{
    public class SummaryController : Controller
    {
        private readonly IOrderHeaderService _service;
        public SummaryController(IOrderHeaderService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult summery()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> summery(CartVm model)
        {
            var data=await _service.SummeryPage(model);
            if (data == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
