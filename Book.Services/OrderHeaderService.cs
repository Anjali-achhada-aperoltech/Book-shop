using Azure;
using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            //var domain = "https://localhost:7071/";
            //var options = new SessionCreateOptions
            //{
            //    LineItems = new List<SessionLineItemOptions>(),
            //    Mode = "payment",
            //    SuccessUrl = domain + $"cart/success?id={cartDto.OrderHeader.Id}",
            //    CancelUrl = domain + $"cart/Index",
            //};
            //    foreach (var item in cartDto.Carts)
            //{
            //    {
            //        var lineitemsoptions = new SessionLineItemOptions
            //        {
            //            PriceData = new SessionLineItemPriceDataOptions
            //            {
            //                UnitAmount =(item.BookItem.price*100) ,
            //                Currency = "inr",
            //                ProductData = new SessionLineItemPriceDataProductDataOptions
            //                {
            //                    Name = item.BookItem.Name,
            //                },
            //            },
            //            Quantity = 1,
            //        };
            //        options.LineItems.Add(lineitemsoptions);

            //}
            //}


            //var service = new SessionService();
            //Session session = service.Create(options);

            // //Response.Headers.Add("Location", session.Url);
            // //return new StatusCodeResult(303);
            //await unitOfWork.cartReposititory.DeleteAllAsync(cartDto.Carts);




            return cartDto;

        }
        public async Task<object> ordersuccess(Guid id)
        {

            //    var orderHeader = await unitOfWork.orderHeaderRepositiory.FindSingleByAsync(x => x.Id == id);
            //    var service = new SessionService();
            //    Session session = await service.GetAsync(orderHeader.SessionId);
            //    if (session.PaymentStatus.ToLower() == "paid")
            //    {
            //        await orderstatus(id, OrderStatus.StatusApproved, PayementStatus.StatusApproved);
            //    }
            //    Cart cart = (Cart)await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == orderHeader.ApplicationUserId);
            //    return id;
            //}
            //public async Task<object> ordersuccess(Guid id)
            //{
            //    // Retrieve the OrderHeader by its ID
            //    var orderHeader = await unitOfWork.orderHeaderRepositiory.FindSingleByAsync(x => x.Id == id);
            //    if (orderHeader == null)
            //    {
            //        throw new Exception("Order not found.");
            //    }

            //    // Check if SessionId is valid
            //    //if (string.IsNullOrWhiteSpace(orderHeader.SessionId))
            //    //{
            //    //    throw new Exception("Invalid session ID.");
            //    //}

            //    // Initialize the SessionService
            //    var service = new SessionService();

            //    // Retrieve the session using the SessionId
            //    Session session;
            //    try
            //    {
            //        session = service.Get(orderHeader.SessionId);
            //    }
            //    catch (StripeException ex)
            //    {
            //        // Handle possible Stripe exceptions
            //        throw new Exception("Failed to retrieve session from Stripe.", ex);
            //    }

            //    if (session == null)
            //    {
            //        throw new Exception("Session not found.");
            //    }

            //    // Check the payment status
            //    if (session.PaymentStatus != null && session.PaymentStatus.ToLower() == "paid")
            //    {
            //        await orderstatus(id, OrderStatus.StatusApproved, PayementStatus.StatusApproved);
            //    }

            //    // Retrieve the associated cart
            //    Cart cart = (Cart)await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == orderHeader.ApplicationUserId);
            //    if (cart == null)
            //    {
            //        throw new Exception("Cart not found.");
            //    }
            //    List<Cart> cart1 = (List<Cart>)await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == orderHeader.ApplicationUserId);

            //    await unitOfWork.cartReposititory.RemoveRange(cart1);
            //    return id;
            // Initialize the SessionService
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
        //public async Task<object> val(CartVm model)
        //{
        //    //var user = httpContextAccessor.HttpContext.User;
        //    //var data = userManager.GetUserId(user);
        //    //var cart = new CartVm()
        //    //{
        //    //    Carts = await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == data)

        //    //};
        //    ////CustomerEmail = model.OrderHeader.CustomerEmail, // Ensure your model has this property


        //    //cart.OrderHeader.Address = cart.OrderHeader.Address;
        //    //cart.OrderHeader.City = cart.OrderHeader.City;
        //    //cart.OrderHeader.state = cart.OrderHeader.state;
        //    //cart.OrderHeader.phone = cart.OrderHeader.phone;
        //    //return model;



        //}
        public async Task<CartVm> GetallAsync()
        {
            var user = httpContextAccessor.HttpContext.User;
            var data = userManager.GetUserId(user);
            CartVm v1 = new CartVm()
            {
                Carts = await unitOfWork.cartReposititory.FindByAsync(x => x.ApplicationuserId == data && !x.IsDeleted, includeProperties: "BookItem")
            };
            //foreach (var item in v1.Carts)
            //{
            //    v1.Total += (double)(item.BookItem.price * item.quantity);
            //}
            return v1;

        }
      
        

    }

}









