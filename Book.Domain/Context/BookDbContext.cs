using Book.Domain.Models;
using Book_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Context
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext>options):base(options)
        {
            
        }
        public BookDbContext()
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookItems> BookItems { get; set; }

    }

}
