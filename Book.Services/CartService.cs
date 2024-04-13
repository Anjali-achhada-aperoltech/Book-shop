using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ROMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class CartService : ServiceBase, ICartService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<Users> userManager;
        public CartService(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor,UserManager<Users>userManager) : base(unitOfWork)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<CartDto> InsertAsync(CartDto cart, Guid Id)
        {
            try
            {
                cart.Id = Guid.NewGuid();
                cart.BookitemId = Id;
                var user = httpContextAccessor.HttpContext.User;
                var data = userManager.GetUserId(user);

                var relatedData =await  unitOfWork.cartReposititory.FindByAsync(x => x.userId == data);

                if (string.IsNullOrEmpty(data))
                    throw new InvalidOperationException("User is not authenticated");
                var userExists = await userManager.FindByIdAsync(data);
                if (userExists == null)
                    throw new InvalidOperationException("User does not exist");
                var existingCart = await unitOfWork.cartReposititory
                 .FindByAsync(c=>c.userId==data && c.BookitemId==cart.BookitemId);

                //if (existingCart != null)
                //{
                    
                //    cart.quantity = IncreMenttItem(cart, cart.quantity);

                //}
                //else
                //{

                    Cart newCart = new Cart
                    {
                        //userId = data,
                        BookitemId = Id,
                        quantity = 1
                    };

                    await unitOfWork.cartReposititory.AddAsync(newCart);
                
                
                return cart;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int IncreMenttItem(CartDto cart, int quantity)
        {
            cart.quantity += quantity;
            return cart.quantity;
        }
    }
    }

