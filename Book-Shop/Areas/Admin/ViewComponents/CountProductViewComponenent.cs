using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Book_Shop.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "CountProduct")]
    public class CountProductViewComponenent:ViewComponent
    {
        private readonly IBookItemService _Service;
        public CountProductViewComponenent(IBookItemService service)
        {
            _Service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _Service.GetAllAsync();
            int totalbooks = books.Count;
            return View(totalbooks);
        }

    }
}
