using Book.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> InsertAsync(CartDto cart,Guid Id);
        Task<CartVm> GetallAsync();
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IncrementCartItem(Guid id);
        Task<bool>DecreMentItem( Guid id);
        Task<int> GetQuantity(Guid userid);




    }
}
