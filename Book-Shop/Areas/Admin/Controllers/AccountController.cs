using Book.Business.DTO;
using Book.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<Applicationuser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.Roles = roles;

            var userList = users.Select(user => new ApplicationUserDTO
            {
                id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault() ?? "User"
            }).ToList();

            return View(userList);
        }

        [HttpPost]
        public IActionResult ChangeRole(string id, string role)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                var currentRoles = _userManager.GetRolesAsync(user).Result;
                _userManager.RemoveFromRolesAsync(user, currentRoles).Wait();
                _userManager.AddToRoleAsync(user, role).Wait();

                TempData["SuccessMessage"] = "Role updated successfully!";
            }

            return RedirectToAction("Index"); 
        }


        // Add New Role
        [HttpPost]
        public async Task<IActionResult> AddRole(string newRole)
        {
            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(newRole));
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User";

            var viewModel = new ApplicationUserDTO
            {
                id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Role = currentRole
            };

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
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
            var data = await _userManager.FindByIdAsync(model.id);
            if (data == null)
            {
                return NotFound();
            }
           var result= await _userManager.DeleteAsync(data);
            if (result.Succeeded)
            {
               
                TempData["SuccessMessage"] = "User deleted successfully.";
                return RedirectToAction("Index");  
            }
            else
            {
               
                TempData["ErrorMessage"] = "Error deleting user. Please try again.";
                return View(model); 
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id); 
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles); 
                await _userManager.AddToRoleAsync(user, role); 

                TempData["SuccessMessage"] = "User role updated successfully!";
            }
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var data = await _userManager.FindByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            
            var currentRole = (await _userManager.GetRolesAsync(data)).FirstOrDefault();

            ApplicationUserDTO model = new ApplicationUserDTO()
            {
                id = data.Id,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                DateOfBirth = data.DateOfBirth,
                Role = currentRole  
            };

            // Pass available roles to the view
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUserDTO model)
        {
            var data = await _userManager.FindByIdAsync(model.id);
            if (data == null)
            {
                return NotFound();
            }

            // Update user properties
            data.FirstName = model.FirstName;
            data.LastName = model.LastName;
            data.DateOfBirth = model.DateOfBirth;
            data.Email = model.Email;

            // Update role if it has changed
            var currentRoles = await _userManager.GetRolesAsync(data);
            if (!currentRoles.Contains(model.Role))
            {
                await _userManager.RemoveFromRolesAsync(data, currentRoles);
                await _userManager.AddToRoleAsync(data, model.Role);
            }

            var result = await _userManager.UpdateAsync(data);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Error updating user. Please try again.";
                // Ensure ViewBag.Roles is passed back when returning to the view.
                ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(model);
            }
        }



    }
}

