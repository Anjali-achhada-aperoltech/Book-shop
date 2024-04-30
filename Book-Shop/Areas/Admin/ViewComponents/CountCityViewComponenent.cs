using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace Book_Shop.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "CountCity")]
    public class CountCityViewComponenent:ViewComponent
    {
        private readonly ICategoryService _service;
        public CountCityViewComponenent(ICategoryService service)
        {
            _service= service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category =await _service.GetAllAsync();
            int totalcategoies=category.Count;
            return View(totalcategoies);
        }



    }
}
