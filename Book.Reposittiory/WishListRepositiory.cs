using Book.Domain.Context;
using Book.Domain.Models;
using Book.Interfaces.Repositiory;
using ROMS.Repositories;


namespace Book.Reposittiory
{
    public class WishListRepositiory : BaseRepository<WishList>, IwishlistRepositiory
    {
        public WishListRepositiory(BookDbContext context) : base(context)
        {
        }
    }
}
