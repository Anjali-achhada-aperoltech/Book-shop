using Book.Business.DTO;
using Book.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Book_Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        public AccountController(UserManager<Users>userManager,SignInManager<Users>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO v1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkemail=await _userManager.FindByEmailAsync(v1.Email);
                    if (checkemail != null)
                    {
                        ModelState.AddModelError(string.Empty, "Email is already exists");
                        return View(v1);
                    }
                    var users = new Users
                    {
                        UserName=v1.Email,
                        Email=v1.Email,
                        FirstName=v1.FirstName,
                        LastName=v1.LastName,
                        DateOfBirth=v1.DateOfBirth.Value

                    };
                    var result = await _userManager.CreateAsync(users, v1.Password);
                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(users,isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    if (result.Errors.Count() > 0)
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                throw;
            }
            return View(v1);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginDto vm)
        {
            try
            {
                Users checkemail = await _userManager.FindByEmailAsync(vm.Email);
                if (checkemail == null) {
                    ModelState.AddModelError(string.Empty, "Email not found");
                    return View(vm);
                }
                if(await _userManager.CheckPasswordAsync(checkemail, vm.Password) == false)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Credentials");
                    return View(vm);
                }   
                var result=await _signInManager.PasswordSignInAsync(vm.Email,vm.Password,vm.RememberMe,lockoutOnFailure
                    :false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
               
               ModelState.AddModelError(string.Empty,"Email and password is wrong");
            
            }
            catch(Exception ex)
            {
                throw;
            }
            return View();
        }
    }

}
