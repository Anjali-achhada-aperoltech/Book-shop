﻿using Azure;
using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ROMS.Services;
using Stripe;
using Stripe.Checkout;
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
        public OrderHeaderService(IUnitOfWork unitOfWork, UserManager<Applicationuser> _usermanager, IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            userManager = _usermanager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderHeader> payementvalue(Guid id, CartVm model, string sessionId, string PaymentId)
        {
            var orderheader = await unitOfWork.orderHeaderRepositiory.FindSingleByAsync(x => x.Id == id);
            orderheader.PaymentIntentId = PaymentId;
            orderheader.SessionId = sessionId;
            return orderheader;
        }
        public async Task<List<CartItemDto>> GetAllCartDetails()
        {
            var user = httpContextAccessor.HttpContext.User;
            var userId = userManager.GetUserId(user);

            List<CartItemDto> list = new List<CartItemDto>();

            var getCart = await unitOfWork.cartReposititory.FindByAsync(x => !x.IsDeleted && x.ApplicationuserId == userId, includeProperties: "BookItem");

            foreach (var item in getCart)
            {
                list.Add(new CartItemDto
                {
                    Name = item.BookItem?.Name ?? "Unknown", // Handle null reference
                    price = item.BookItem?.price ?? 0, // Ensure price is not null
                    Quantity = item.quantity ?? 1, // Default to 1 if null
                    Total = (item.BookItem?.price ?? 0) * (item.quantity ?? 1) // Compute total safely
                });
            }

            return list;
        }

        public async Task<CartVm> SummeryPage(CartVm cartDto)
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
                    Id = Guid.NewGuid(),
                    BookItemId = item.BookItem.Id,
                    OrderHeaderId = cartDto.OrderHeader.Id,
                    quantity = (int)item.quantity,
                    price = item.BookItem.price

                };

                await unitOfWork.orderDetailRepositiory.AddAsync(orderDetail);
                await unitOfWork.cartReposititory.RemoveRange(cartDto.Carts);
            }



            return cartDto;

        }
        public async Task<object> ordersuccess(Guid id)
        {
            var service = new SessionService();

            // Retrieve the session using the SessionId
            Session session;
            try
            {
                var orderHeader = await unitOfWork.orderHeaderRepositiory.FindSingleByAsync(x => x.Id == id);

                if (string.IsNullOrWhiteSpace(orderHeader.SessionId))
                {
                    throw new Exception("Invalid session ID.");
                }

                session = service.Get(orderHeader.SessionId); // Ensure the session ID is not null or empty


                if (session == null)
                {
                    throw new Exception("Session not found.");
                }
                if (session.PaymentStatus != null && session.PaymentStatus.ToLower() == "paid")
                {
                    await orderstatus(id, OrderStatus.StatusApproved, PayementStatus.StatusApproved);
                }

                // Retrieve the associated cart
                Cart cart = (Cart)await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == orderHeader.ApplicationUserId);
                if (cart == null)
                {
                    throw new Exception("Cart not found.");
                }
                List<Cart> cart1 = (List<Cart>)await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == orderHeader.ApplicationUserId);

                await unitOfWork.cartReposititory.RemoveRange(cart1);
                return id;
            }
            catch (StripeException ex)
            {
                // Handle possible Stripe exceptions more specifically
                throw new Exception($"Failed to retrieve session from Stripe: {ex.Message}", ex);
            }

        }


        public async Task<OrderHeader> orderstatus(Guid id, string OrderStatus, string? PayementStatus = null)
        {
            var order = await unitOfWork.orderHeaderRepositiory.FindSingleByAsync(x => x.Id == id);
            if (order != null)
            {
                order.OrderStatus = OrderStatus;
            }
            if (PayementStatus != null)
            {
                order.PaymentStatus = PayementStatus;
            }
            return order;
        }
        public async Task<OrderDetailsDTO> GetOrderDetailsAsync(Guid id)
        {
            var order = await unitOfWork.orderHeaderRepositiory.GetAsync(id);
            if (order == null) return null;

            var user = await userManager.Users.FirstOrDefaultAsync(m => m.Id == order.ApplicationUserId);

            return new OrderDetailsDTO
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
        }

        // Delete order
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await unitOfWork.orderHeaderRepositiory.GetAsync(id);
            if (order == null) return false;

            await unitOfWork.orderHeaderRepositiory.DeleteAsync(order,true);

            return true;
        }

        
        public async Task<CartVm> GetallAsync()
        {
            var user = httpContextAccessor.HttpContext.User;
            var data = userManager.GetUserId(user);
            CartVm v1 = new CartVm()
            {
                Carts = await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == data && !x.IsDeleted, includeProperties: "BookItem")
            };
            
            return v1;

        }
      
        

    }

}









