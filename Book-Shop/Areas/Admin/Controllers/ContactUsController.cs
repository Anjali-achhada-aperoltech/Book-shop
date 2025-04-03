using Book.Business.DTO;
using Book.Interfaces.Services;
using Book.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;
        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _contactUsService.GetAllAsync();
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
        public async Task<IActionResult> Create(ContactUsDTO c1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _contactUsService.AddAsync(c1);
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

                var data = await _contactUsService.GetAsync(id);
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ContactUsDTO c1)
        {
            var data = await _contactUsService.UpdateAsync(c1);
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
        public async Task<IActionResult> Delete(ContactUsDTO c1, Guid id)
        {
            var data = await _contactUsService.DeleteAsync(id);
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
            var data = await _contactUsService.GetAsync(id);
            return View(data);
        }
    
}
}
