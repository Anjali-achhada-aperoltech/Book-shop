using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Repositiory;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]


    public class OrderController : Controller
    {
        private readonly IOrderHeaderService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<Applicationuser> userManager;
        public OrderController(IOrderHeaderService service, IUnitOfWork unitOfWork, IHttpContextAccessor context, UserManager<Applicationuser> userManager)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
            this._context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch all orders once
            var orders = await unitOfWork.orderHeaderRepositiory.GetAllAsync();
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderDetailsDTO> vm = new List<OrderDetailsDTO>();

            // Preload all user IDs in the orders to minimize database calls
            var userIds = orders.Select(o => o.ApplicationUserId).Distinct();
            var users = new Dictionary<string, Applicationuser>();
            foreach (var userId in userIds)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(m => m.Id == userId);
                if (user != null)
                {
                    users[userId] = user;
                }
            }

            // Create the view model list
            foreach (var order in orders)
            {
                var user = users.ContainsKey(order.ApplicationUserId) ? users[order.ApplicationUserId] : null;
                var orderUserDetails = new OrderDetailsDTO
                {
                    Id = order.Id,
                    Applicationuserid = order.ApplicationUserId,
                    FirstName = user?.FirstName,
                    LastName = user?.LastName,
                    email = user?.Email,
                    phoneno = order.phone,
                    Address = order.Address,
                    city = order.City,
                    state = order.state,
                    OrderStatus = order.OrderStatus,
                    OrderDate = order.DateOfOrder,
                    OrderTotal = order.OrderTotal
                };
                vm.Add(orderUserDetails);
            }

            return View(vm);
        }
       



    }
}


