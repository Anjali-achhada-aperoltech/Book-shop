using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class ViewOrderDetailsDto
    {
        public Guid Id { get; set; }
        public int TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
        public int Quantity { get; set; }
    }
    
}
