using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "CountBookLanguage")]
    public class CountBookLanguageViewComponent : ViewComponent // ✅ FIX: Inherit from ViewComponent
    {
        private readonly IBookLanguageService _service;

        public CountBookLanguageViewComponent(IBookLanguageService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _service.GetAllAsync();
            int totalCategories = category.Count;

            return View(totalCategories); // ✅ FIX: No explicit cast
        }
    }
}
