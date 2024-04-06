using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROMS.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Guid id);

        Task<TEntity> AddAsync(TEntity model);

        Task<bool> UpdateAsync(TEntity model);

        Task<bool> DeleteAsync(Guid id);
    }
}
