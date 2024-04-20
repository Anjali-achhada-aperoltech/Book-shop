using Book.Business.DTO;
using Book.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<Applicationuser> userManager;
        public AccountController(UserManager<Applicationuser>userManager)
        {
            this.userManager=userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<ApplicationUserDTO> a1 = new List<ApplicationUserDTO>();

            var users =await  userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                a1.Add(new ApplicationUserDTO {id=user.Id, Email=user.Email,FirstName=user.FirstName,LastName=user.LastName,DateOfBirth=user.DateOfBirth });
            }
            return View(a1);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new ApplicationUserDTO
            {
                id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName= user.LastName,
                DateOfBirth=user.DateOfBirth,
                // Map other fields you wish to display
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult>Delete(ApplicationUserDTO model)
        {
            var data = await userManager.FindByIdAsync(model.id);
            if (data == null)
            {
                return NotFound();
            }
           var result= await userManager.DeleteAsync(data);
            if (result.Succeeded)
            {
                // Consider using TempData to pass success messages if redirecting
                TempData["SuccessMessage"] = "User deleted successfully.";
                return RedirectToAction("Index");  // Assuming 'Index' is your user list view
            }
            else
            {
                // Handle the case when the deletion fails
                TempData["ErrorMessage"] = "Error deleting user. Please try again.";
                return View(model);  // You might want to return to a view that displays the error
            }

        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var data=await userManager.FindByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            ApplicationUserDTO model = new ApplicationUserDTO()
            {
                Email=data.Email,
                FirstName=data.FirstName,
                LastName=data.LastName,
                DateOfBirth=data.DateOfBirth,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Update(ApplicationUserDTO model)
        {
            var data = await userManager.FindByIdAsync(model.id);
            if (data == null)
            {
                return NotFound();
            }
            data.FirstName = model.FirstName;
            data.LastName = model.LastName;
            data.DateOfBirth = model.DateOfBirth;
            data.Email = model.Email;
          var result=  await userManager.UpdateAsync(data);
            if (result.Succeeded)
            {
                // Consider using TempData to pass success messages if redirecting
                TempData["SuccessMessage"] = "User updated  successfully.";
                return RedirectToAction("Index");  // Assuming 'Index' is your user list view
            }
            else
            {
                // Handle the case when the deletion fails
                TempData["ErrorMessage"] = "Error deleting user. Please try again.";
                return View(model);  // You might want to return to a view that displays the error
            }

        }
    }
    }

