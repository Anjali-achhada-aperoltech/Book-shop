using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public static class OrderStatus
    {
        public const string StatusPending = "Pending";
        public const string StatusRefund = "Refunded";
        public const string StatusApproved = "Approved";
        public const string StatusCanelled = "Cancelled";
        public const string StatusInProcess = "UnderProcessing";
        public const string StatusShapped = "Shipped";

    }
}
