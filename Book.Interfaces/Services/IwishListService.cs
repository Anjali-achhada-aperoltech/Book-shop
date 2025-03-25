using Book.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface IwishListService
    {
        Task<bool> AddToWishlist(Guid bookId);
        Task<bool> RemoveFromWishlist( Guid bookId);
        Task<int> GetWishlistCount();
        Task<WishListDTO> GetAllWishList();
    }
}
