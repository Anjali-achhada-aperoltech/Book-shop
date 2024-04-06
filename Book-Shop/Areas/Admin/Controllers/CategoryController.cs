using Book.Business.DTO;
using Book.Interfaces.Services;
using Book_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService service;
        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await service.GetAllAsync();
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
        public async Task<IActionResult>Create(CategoryDto c1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await service.AddAsync(c1);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["success"] = "Data is inserted successfully";
                        return RedirectToAction("Index");
                    }
                }

            }catch (Exception ex)
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
                var data = await service.GetAsync(id);
                return View(data);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult>Edit(CategoryDto c1)
        {
            var data = await service.UpdateAsync(c1);
            if (data == true)
            {
                TempData["update"] = "Update data successfully";
                return RedirectToAction("Index");
            }
           

            return View();
           
        }
        [HttpGet]
        public async Task<IActionResult>Delete(Guid id)
        {
            var data = await service.GetAsync(id);
            if(data== null)
            {
                return NoContent();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryDto c1,Guid id)
        { 
            var data=await service.DeleteAsync(id);
            if (data == true)
            {
                TempData["delete"] = "Delete Data Successfully";
                return RedirectToAction("Index");
            }
            return View(data);



        }
        public async Task<IActionResult> Details(Guid id)
        {
            var data = await service.GetAsync(id);
            return View(data);
        }


    }
}
