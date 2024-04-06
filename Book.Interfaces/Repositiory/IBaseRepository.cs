using ROMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ROMS.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, string includeProperties = "");

        Task<T> GetAsync(Guid id, string includeProperties = "");

        Task<T> AddAsync(T entity);

        Task<int> AddRangeAsync(IEnumerable<T> entities);

        Task<int> DeleteAsync(T entity, bool isHardDelete = false);

        Task<bool> DeleteAllAsync(IEnumerable<T> entities, bool isHardDelete = false);

        Task<int> UpdateAsync(T entity);

        Task<int> SaveAsync();
    }
}
