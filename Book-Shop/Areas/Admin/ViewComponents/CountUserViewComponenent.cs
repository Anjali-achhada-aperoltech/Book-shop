using Book.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Book_Shop.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "CountUser")]
    public class CountUserViewComponenent : ViewComponent
    {
        private readonly UserManager<Applicationuser> userManager;
        public CountUserViewComponenent(UserManager<Applicationuser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var users =await userManager.Users.ToListAsync();
            var total = users.Count();
            return View(total);

        }

    }
}