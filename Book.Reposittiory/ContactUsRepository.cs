using Book.Domain.Context;
using Book.Domain.Models;
using Book.Interfaces.Repositiory;
using ROMS.Repositories;

namespace Book.Reposittiory
{

    public class ContactUsRepository : BaseRepository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(BookDbContext context) : base(context)
        {
        }
    }
}
