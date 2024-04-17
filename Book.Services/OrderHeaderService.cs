using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ROMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class OrderHeaderService : ServiceBase, IOrderHeaderService
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OrderHeaderService(IUnitOfWork unitOfWork,UserManager<Applicationuser>_usermanager,IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            userManager = _usermanager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartVm> SummeryPage(CartVm cartDto)
        {
            try
            {
                var user = httpContextAccessor.HttpContext.User;
                var data = userManager.GetUserId(user);


                cartDto.Carts = await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == data, includeProperties: "BookItem");

                cartDto.OrderHeader = new OrderHeader
                {
                    Id = Guid.NewGuid(),
                    OrderStatus = OrderStatus.StatusPending,
                    PaymentStatus = PayementStatus.StatusPending,
                    DateOfOrder = DateTime.Now,
                    ApplicationUserId = data,
                    Address = cartDto.Address,
                    phone = cartDto.phone,
                    City = cartDto.City,
                    state = cartDto.state
                };
                foreach (var item in cartDto.Carts)
                {
                    cartDto.OrderHeader.OrderTotal += (double)(item.BookItem.price * item.quantity);
                }
                await unitOfWork.orderHeaderRepositiory.AddAsync(cartDto.OrderHeader);
                foreach (var item in cartDto.Carts)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        Id=Guid.NewGuid(),
                        BookItemId = item.BookItem.Id,
                        OrderHeaderId = cartDto.OrderHeader.Id,
                        quantity = (int)item.quantity,
                        price = item.BookItem.price

                    };

                    await unitOfWork.orderDetailRepositiory.AddAsync(orderDetail);
                }

                return cartDto;

            }catch(Exception ex)
            {
                throw;
            }
           
        }
    }
}
    
