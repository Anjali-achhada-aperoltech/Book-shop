using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class CartDto:BaseDTO
    {
      
        public Guid BookitemId { get; set; }
        public int quantity {  get; set; }
        public string? userId { get; set; }


    }
}
