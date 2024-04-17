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
        public OrderHeader OrderHeader { get; set; }
        public int phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public string state { get; set; }
    }
}
