using Book.Business.DTO;
using Book.Interfaces.Services;
using Book.UOW;
using ROMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class BookItemsService : ServiceBase,IBookItemService
    {
        public BookItemsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<BookItemsDTO> AddAsync(BookItemsDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CreateBookDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CreateBookDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(BookItemsDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
