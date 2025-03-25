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
        ISubCategoryRepository SubCategoryRepository { get; }
        IBookItemsRepositiory bookItemsRepositiory { get; }
        ICartReposititory cartReposititory { get; }
        IOrderHeaderRepositiory orderHeaderRepositiory { get; }
        IOrderDetailRepositiory orderDetailRepositiory { get; }
        IAboutUsRepostitory aboutUsRepostitory { get; }
        IBookLanguageRepostiory bookLanguageRepostiory { get; }

    }
}
