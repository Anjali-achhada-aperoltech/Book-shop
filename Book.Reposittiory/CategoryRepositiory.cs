using Book.Domain.Context;
using Book.Interfaces.Repositiory;
using Book_Shop.Models;
using ROMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Reposittiory
{
    public class CategoryRepositiory : BaseRepository<Category>,ICategoryReposititory
    {
        public CategoryRepositiory(BookDbContext context) : base(context)
        {
        }
    }
}
