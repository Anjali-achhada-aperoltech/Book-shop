using Book.Domain.Context;
using Book.Domain.Models;
using Book.Interfaces.Repositiory;
using ROMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Reposittiory
{
    public class SubCategoryRepository : BaseRepository<SubCategory>,ISubCategoryRepository
    {
        public SubCategoryRepository(BookDbContext context) : base(context)
        {
        }
    }
}
