using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Book_Shop.Controllers
{
    public class ViewOrderController : Controller
    {
        private readonly IOrderHeaderService service;
        public ViewOrderController(IOrderHeaderService service)
        {
            this.service = service;
        }
        [Authorize]

        public async Task<IActionResult> Index()
        {
            var orders = await service.Vieworder();
            return View(orders);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var order = await service.ViewOrderAsync(id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var data = await service.CancelOrder(id);
            if (data)
            {
                TempData["AlertMessage"] = "Order canceled successfully!";
                return RedirectToAction("Index"); // Redirect to the order list
            }

            TempData["ErrorMessage"] = "Failed to cancel the order!";
            return RedirectToAction("ViewOrderDetails", new { id });
        }

    }
}

