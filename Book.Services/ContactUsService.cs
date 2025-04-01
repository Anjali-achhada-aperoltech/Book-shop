using Book.Business.DTO;
using Book.Interfaces.Services;
using Book.UOW;
using ROMS.Interfaces.Services;
using ROMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class ContactUsService : ServiceBase, IContactUsService
    {
        public ContactUsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<ContactUsDTO> AddAsync(ContactUsDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContactUsDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ContactUsDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ContactUsDTO model)
        {
            throw new NotImplementedException();
        }
    }
}