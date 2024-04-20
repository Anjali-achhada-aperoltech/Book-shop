using Book.Business.DTO;
using Book.Domain.Models;
using ROMS.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface IOrderHeaderService
    {
        Task<CartVm>SummeryPage(CartVm cartDto);
        Task<OrderHeader> payementvalue(Guid id, CartVm model, string sessionId, string PaymentId);
        Task<OrderHeader> orderstatus(Guid id, string OrderStatus, string? PayementStatus = null);
        Task<object> ordersuccess(Guid id);
       // Task<object> val(CartVm model);
    }
}
