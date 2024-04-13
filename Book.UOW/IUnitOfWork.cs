using Book.Interfaces.Repositiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryReposititory CategoryReposititory { get; }
        IBookItemsRepositiory bookItemsRepositiory { get; }
        ICartReposititory cartReposititory { get; }
    }
}
