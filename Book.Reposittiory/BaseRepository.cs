using ROMS.Domain;

using ROMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Book.Domain.Context;

namespace ROMS.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : BaseEntity
    {
        protected BookDbContext Context;

        protected BaseRepository(BookDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includeProperties = "")
        {

            IQueryable<T> query = Context.Set<T>();

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            try
            {
                return await query.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
       


        public async Task<T> GetAsync(Guid id, string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();

            // Apply includes based on the includeProperties string
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim()); // Ensure to trim spaces
                }
            }
          
           var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await Context.Set<T>().AddAsync(entity);
                await Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity, bool IsHardDelete = false)
        {
            if (IsHardDelete)
            {
                Context.Set<T>().Remove(entity);
                return await Context.SaveChangesAsync();
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
                return await Context.SaveChangesAsync();
            }

        }

        public async Task<bool> DeleteAllAsync(IEnumerable<T> entities, bool IsHardDelete = false)
        {
            foreach (var entity in entities)
            {
                await DeleteAsync(entity, IsHardDelete);
            }
            return true;
        }
        public async Task<bool> RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
            try
            {
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception here; for example, using ILogger
                return false;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public async Task<T> FindSingleByAsync(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            try
            {
                return await query.SingleOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it specifically
                throw;
            }
        }

    }
}
