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
        Task<OrderSuccessResponse> ordersuccess(Guid id);
        Task<CartVm> GetallAsync();
        Task<List<CartItemDto>> GetAllCartDetails();
        Task<OrderDetailsDTO> GetOrderDetailsAsync(Guid id);
        Task<bool> DeleteOrderAsync(Guid id);
        Task<List<OrderDto>> Vieworder();
        Task<ViewOrderDetailsDto> ViewOrderAsync(Guid id);
        Task<bool> CancelOrder(Guid orderDetailId);
        }
}
