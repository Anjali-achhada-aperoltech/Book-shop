using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AboutController : Controller
    {
        private readonly IAboutUsService _aboutserive;
        public AboutController(IAboutUsService aboutUsService)
        {
            _aboutserive = aboutUsService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _aboutserive.GetAllAsync();
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(AboutUsDto a1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _aboutserive.AddAsync(a1);
                    if (data == null)
                    {
                        TempData["NameExist"] = "Name is already Exist";
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

                var data = await _aboutserive.GetAsync(id);
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AboutUsDto c1)
        {
            var data = await _aboutserive.UpdateAsync(c1);
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
        public async Task<IActionResult> Delete(AboutUsDto c1, Guid id)
        {
            var data = await _aboutserive.DeleteAsync(id);
            if (data == true)
            {
                TempData["delete"] = "Delete Data Successfully";
                return RedirectToAction("Index");
            }
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var data = await _aboutserive.GetAsync(id);
            return View(data);
        }
    }
    
}
