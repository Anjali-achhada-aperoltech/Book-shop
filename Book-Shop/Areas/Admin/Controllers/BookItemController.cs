using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class BookItemController : Controller
    {
        private readonly IBookItemService _service;
        private readonly ICategoryService _categoryService;
        private readonly IBookLanguageService _languageService;
        private readonly IWebHostEnvironment _environment;


        public BookItemController(IBookItemService service, ICategoryService _categoryservice, IWebHostEnvironment _environment, IBookLanguageService languageService)
        {
            this._service = service;
            this._categoryService = _categoryservice;
            this._environment = _environment;
            _languageService = languageService;
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
            var getbooklanaguage=await _languageService.GetAllAsync();
            ViewBag.data = categries.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            ViewBag.booklanguage = getbooklanaguage.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookItemsDTO b1, IFormFile FrontImage)
        {

            var imagePath = await SaveImageAsync(FrontImage);
            b1.FrontImage = imagePath;
            var data = await _service.AddAsync(b1);
            if (data == null)
            {
                    TempData["NameExist"] = "Name is already Exist";
            }
            else
            {
                TempData["success"] = "Data is inserted successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<string> SaveImageAsync(IFormFile FrontImage)
        {
            string imageurl = null;
            var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(FrontImage.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await FrontImage.CopyToAsync(fileStream);
            }

            // Return relative path instead of full file system path
            return "/uploads/" + fileName;
        }

        private async Task<string> EditImageAsync(IFormFile FrontImage, string existingImagePath)
        {
            if (FrontImage == null)
            {
                return existingImagePath;
            }

            string uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(FrontImage.FileName);
            string filePath = Path.Combine(uploadsFolderPath, fileName);

            
            if (!string.IsNullOrEmpty(existingImagePath))
            {
                string oldFilePath = Path.Combine(_environment.WebRootPath, existingImagePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await FrontImage.CopyToAsync(fileStream);
            }

            return "/uploads/" + fileName; 
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
                TempData["delete"] = "Delete Data Sucessfully";
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
            var categories = await _categoryService.GetAllAsync();
            var languages = await _languageService.GetAllAsync();

            ViewBag.data = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            ViewBag.booklanguage = languages.Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.Id.ToString()
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
        public async Task<IActionResult> Edit(BookItemsDTO c1, IFormFile FrontImage)
        {
            var existingData = await _service.GetItemsAsync(c1.Id);
            if (existingData == null)
            {
                return NotFound();
            }
            c1.FrontImage = await EditImageAsync(FrontImage, existingData.FrontImage);

            var data = await _service.UpdateAsync(c1);
            if (data)
            {
                TempData["update"] = "Update data successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["NameExist"] = "Name already exists";
            }

            return View();
        }

    }
}