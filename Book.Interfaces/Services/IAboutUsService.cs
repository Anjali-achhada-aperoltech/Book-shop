using Book.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface IAboutUsService
    {
        Task<List<AboutUsDto>> GetAllAsync();

        Task<AboutUsDto> GetAsync(Guid id);
        Task<AboutUsDto> AddAsync(AboutUsDto model);

        Task<bool> UpdateAsync(AboutUsDto model);

        Task<bool> DeleteAsync(Guid id);
    }
}
