using Book.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class CartVm:BaseDTO
    {
        public IEnumerable<Cart> Carts { get; set; }
        public double Total {  get; set; }
    }
}
