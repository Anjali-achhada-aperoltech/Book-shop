using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Humanizer;
using Book.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Book_Shop.Controllers
{
    public class SummaryController : Controller
    {
        private readonly IOrderHeaderService _service;
        private readonly ICartService cartService;
       
        
        public SummaryController(IOrderHeaderService service, ICartService cartService) { 
            _service = service;
            this.cartService = cartService;
           
        }
        [HttpGet]
        public IActionResult summery()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> summery(CartVm model)
        //{
        //    var data=await _service.SummeryPage(model);
        //    await _service.payementvalue(model.OrderHeader.Id,sessionId,PaymentId);
        //    Response.Headers.Add("Location", session.Url);
        //    return new StatusCodeResult(303);

        //}
        [HttpPost]
        public async Task<IActionResult> summery(CartVm model)
        {
            try
            {
                // Assuming _service.SummeryPage initializes the Cart and returns a filled model with OrderHeader etc.
                var cartDto = await _service.SummeryPage(model);
                if (cartDto == null)
                {
                    return NotFound("Cart details not found.");
                }

                // Prepare Stripe options for session creation
                var options = new SessionCreateOptions
                {
                    // Un-comment and use if you need to specify the payment method types explicitly.
                    // PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = "https://localhost:7071/Summary/success?id=" + model.OrderHeader.Id,
                    CancelUrl = "https://localhost:7071/home/index",
                   
                    ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string> {  "IN" }, // Specify allowed countries here
                    }
                };

                // Note: Ensure 'model.OrderHeader' contains all these fields and they are not null or empty.


                foreach (var item in cartDto.Carts)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.BookItem.price * 100), // Convert to smallest currency unit, e.g., cents
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.BookItem.Name,
                            },
                        },
                        Quantity = item.quantity,
                    });
                }

                // Create the Stripe session
                var service = new SessionService();
                Session session = service.Create(options);
                await _service.payementvalue(model.OrderHeader.Id, model, session.Id, session.PaymentIntentId);

                // Redirect to Stripe checkout
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            catch (StripeException ex)
            {
                if (ex.StripeError?.Code == "address_invalid" || ex.StripeError?.Type == "invalid_request_error")
                {
                    return BadRequest($"Stripe compliance error: {ex.StripeError.Message}");
                }
                else
                {
                    return BadRequest($"Stripe error: {ex.Message}");
                }
            
            // Log the exception or handle it according to your scenario
            //return BadRequest("Stripe error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // General exception handling, log this exception as well
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
            
        }
        //[HttpGet]
        //public async Task<IActionResult> success(Guid id)
        //{
        //    var data =  await _service.ordersuccess(id);
        //    return View(id);
        //}
        [HttpGet]
        public async Task<IActionResult> success(Guid id)
        {
            var data = await _service.ordersuccess(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }
        

        

    }

}
