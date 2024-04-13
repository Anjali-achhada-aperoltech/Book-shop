using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookItemController : Controller
    {
        private readonly IBookItemService _service;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;


        public BookItemController(IBookItemService service, ICategoryService _categoryservice, IWebHostEnvironment _environment)
        {
            this._service = service;
            this._categoryService = _categoryservice;
            this._environment = _environment;
        }
        public async Task<IActionResult> Index()
        {

            var data = await _service.GetAllAsync();
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
            var categries = await _categoryService.GetAllAsync();
            ViewBag.data = categries.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookItemsDTO b1, IFormFile FrontImage)
        {
            try
            {
                var imagePath = await SaveImageAsync(FrontImage);
                b1.FrontImage = imagePath; // Assuming FrontImage is meant to hold the path

                var data = await _service.AddAsync(b1);
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
            catch (Exception ex)
            {
                throw;
            }


            return View();
        }
        public async Task<string> SaveImageAsync(IFormFile FrontImage)
        {
            string imageurl=null;
            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(FrontImage.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await FrontImage.CopyToAsync(fileStream);
            }

            return filePath;
        }
        private async Task<string> EditImageAsync(IFormFile FrontImage)
        {
            string imageurl = null;
            if (FrontImage != null)
            {
                imageurl = Path.Combine("uploads", FrontImage.FileName);

                string absoluteFilePath = Path.Combine(_environment.WebRootPath, imageurl);

                if (System.IO.File.Exists(absoluteFilePath))
                {
                    System.IO.File.Delete(absoluteFilePath);
                }
                using (var fileStream = new FileStream(absoluteFilePath, FileMode.Create))
                {
                    await FrontImage.CopyToAsync(fileStream);
                }
            }
            return imageurl;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                return View(string.Empty, "Data Not Found");
            }
            else
            {
                return View(data);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                return View(string.Empty, "Data Not Found");
            }
            else
            {
                return View(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, CreateBookDTO b1)
        {
            if (!string.IsNullOrEmpty(b1.FrontImage))
            {
                if (!DeleteImageFile(b1.FrontImage))
                {

                    return RedirectToAction("Index");
                }
            }
            var data = await _service.DeleteAsync(id);
            if (data == true)
            {
                TempData["delete"] = "Delete Data Successfully";
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public bool DeleteImageFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var val =await _categoryService.GetAllAsync();
            ViewBag.data = val.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            var data = await _service.GetItemsAsync(id);
            if (data == null)
            {
                return View(string.Empty, "Data Not Found");
            }
            else
            {
                return View(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookItemsDTO c1,IFormFile FrontImage)
        {
            
                var imagepath=await EditImageAsync(FrontImage);
                c1.FrontImage=imagepath;
            
            var data = await _service.UpdateAsync(c1);
            if (data == true)
            {
                TempData["update"] = "Update data successfully";
                return RedirectToAction("Index");
            }

            return View();





        }
    }
}