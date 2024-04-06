using Book.Domain.Context;
using Book.Interfaces.Repositiory;
using Book.Reposittiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.UOW
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BookDbContext _context;
        public UnitOfWork(BookDbContext _context)
        {
            this._context = _context;
            CategoryReposititory = new CategoryRepositiory(_context);
            bookItemsRepositiory = new BookItemsRepostitory(_context);
        }
        private bool disposed;
        public ICategoryReposititory CategoryReposititory { get; private set; }
        public IBookItemsRepositiory bookItemsRepositiory { get; private set; }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
