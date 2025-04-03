using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class OrderSuccessResponse
    {
        public Guid OrderId { get; set; }
        public string Message { get; set; }
        public string PaymentStatus { get; set; }

    }
}
