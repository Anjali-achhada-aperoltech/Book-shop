using Book.Domain.Context;
using Book.Domain.Models;
using Book.Interfaces.Repositiory;
using ROMS.Interfaces.Repositories;
using ROMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Reposittiory
{
    public class BookLanguageRepository : BaseRepository<BookLanguage>,IBookLanguageRepostiory
    {
        public BookLanguageRepository(BookDbContext context) : base(context)
        {
        }
    }
}
