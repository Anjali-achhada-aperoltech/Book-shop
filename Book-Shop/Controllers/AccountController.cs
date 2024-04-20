using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Encodings.Web;

namespace Book_Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Applicationuser> _userManager;
        private readonly SignInManager<Applicationuser> _signInManager;
        private readonly IEmailSender emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;
      
        public AccountController(UserManager<Applicationuser> userManager,SignInManager<Applicationuser> signInManager,IEmailSender emailSender,IWebHostEnvironment webHostEnvironment,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.emailSender = emailSender;
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;

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
                    var users = new Applicationuser
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
                        bool status=await emailSender.EmailSenderAsync(v1.Email, "Account Created",await  GetEmailBody(v1.Email));
                         await _signInManager.SignInAsync(users,isPersistent: false);
                        return RedirectToAction("Login", "Account");
                        



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
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
           
            return View();
        }
        public async Task<IActionResult> Login(LoginDto vm, string returnUrl = null)
        {
           
            try
            {
                Applicationuser checkemail = await _userManager.FindByEmailAsync(vm.Email);
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
                    // Redirects to the return URL if it's locally valid; otherwise, redirects to a default page
                    return LocalRedirect(returnUrl ?? "/");
                }
                else
                {
                    return RedirectToAction("Home", "Index");
                }
                ModelState.AddModelError(string.Empty,"Email and password is wrong");
            
            }
            catch(Exception ex)
            {
                throw;
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public async Task<string>GetEmailBody(string email)
        {
            string url = configuration.GetValue<string>("urls:LoginUrl");
            string path = Path.Combine(webHostEnvironment.WebRootPath, "Template\\Welcome.cshtml");
            string htmlstring=System.IO.File.ReadAllText(path);
            htmlstring = htmlstring.Replace("{{title}}","User Registration");
            htmlstring = htmlstring.Replace("{{username}}",email);

            htmlstring = htmlstring.Replace("{{url}}", "https://localhost:7071/account/login");
            return htmlstring;

        }
    }

}
