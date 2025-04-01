using Book.Business.DTO;
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
            SubCategoryRepository = new SubCategoryRepository(_context);
            bookItemsRepositiory = new BookItemsRepostitory(_context);
            cartReposititory = new CartRepostitory(_context);
            orderHeaderRepositiory = new OrderHeaderRepositiory(_context);
            orderDetailRepositiory=new OrderDetailRepositiory(_context);
            aboutUsRepostitory = new AboutUsRepositiory(_context);
            contactUsRepository = new ContactUsRepository(_context);
            bookLanguageRepostiory=new BookLanguageRepository(_context);
            wishlistRepositiory = new WishListRepositiory(_context);
        }
        private bool disposed;
        public ICategoryReposititory CategoryReposititory { get; private set; }
        public ISubCategoryRepository SubCategoryRepository { get; private set; }
        public IBookItemsRepositiory bookItemsRepositiory { get; private set; }
        public ICartReposititory cartReposititory { get; private set; }
        public IOrderHeaderRepositiory orderHeaderRepositiory { get; private set; }
        public IOrderDetailRepositiory orderDetailRepositiory { get; private set; }
        public IAboutUsRepostitory aboutUsRepostitory { get; private set; }
        public IBookLanguageRepostiory bookLanguageRepostiory { get; private set; }
        public IwishlistRepositiory wishlistRepositiory { get; private set; }
        public IContactUsRepository contactUsRepository { get; private set; }
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
