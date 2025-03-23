using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class BookLanguageController : Controller
    {
        private readonly IBookLanguageService _bookLanguageService;
        public BookLanguageController(IBookLanguageService bookLanguageService)
        {
            _bookLanguageService = bookLanguageService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _bookLanguageService.GetAllAsync();
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookLangageDTO c1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _bookLanguageService.AddAsync(c1);
                    if (data == null)
                    {
                        TempData["NameExist"] = "Name is already Exist";
                        return View(c1);
                    }
                    else
                    {
                        TempData["success"] = "Data is inserted successfully";
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

                var data = await _bookLanguageService.GetAsync(id);
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookLangageDTO c1)
        {
            var data = await _bookLanguageService.UpdateAsync(c1);
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
        public async Task<IActionResult> Delete(BookLangageDTO c1, Guid id)
        {
            var data = await _bookLanguageService.DeleteAsync(id);
            if (data == true)
            {
                TempData["delete"] = "Delete Data Successfully";
                return RedirectToAction("Index");
            }
            return View(data);
        }
    }
}

