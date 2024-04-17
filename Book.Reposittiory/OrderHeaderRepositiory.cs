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
    public class OrderHeaderRepositiory : BaseRepository<OrderHeader>,IOrderHeaderRepositiory
    {
        public OrderHeaderRepositiory(BookDbContext context) : base(context)
        {
        }
    }
}
