using Book.Business.DTO;

namespace Book.Interfaces.Services
{
    public interface IContactUsService
    {
        Task<List<ContactUsDTO>> GetAllAsync();
        Task<ContactUsDTO> GetAsync(Guid id);
        Task<ContactUsDTO> AddAsync(ContactUsDTO model);
        Task<bool> UpdateAsync(ContactUsDTO model);
        Task<bool> DeleteAsync(Guid id);
    }
}
