using Book.Business.DTO;
using Book.Interfaces.Services;
using Book.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _CategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _CategoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _subCategoryService.GetAllAsync();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return View(data);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _CategoryService.GetAllAsync();
            ViewBag.Categories = categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = c.Name,  
                Value = c.Id.ToString() 
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryDTO subCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _subCategoryService.AddAsync(subCategory);
                    if (data == null)
                    {
                        TempData["NameExist"] = "Name is already Exist";
                    }
                    else
                    {
                        TempData["success"] = "Data Inserted Successfully";

                        return RedirectToAction("Index");
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var data = await _subCategoryService.GetAsync(id);
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SubCategoryDTO subCategory)
        {
            var data = await _subCategoryService.UpdateAsync(subCategory);
            if (data == false)
            {
                TempData["NameExist"] = "Name already Exist";
            }
            else
            {
                TempData["update"] = "Update data successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SubCategoryDTO subCategory, Guid id)
        {
            var data = await _subCategoryService.DeleteAsync(id);
            if (data == true)
            {
                TempData["delete"] = "Delete Data Successfully";
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var data = await _subCategoryService.GetAsync(id);
            return View(data);
        }
    }
}

