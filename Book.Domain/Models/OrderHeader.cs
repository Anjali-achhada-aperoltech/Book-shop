using ROMS.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Models
{
    public class OrderHeader:BaseEntity
    {
        public string ApplicationUserId {  get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateShipping { get; set; }
        public double OrderTotal {  get; set; }
        public string? OrderStatus {  get; set; }
        public string? PaymentStatus {  get; set; }
        public string? TrackingNumber {  get; set; }
        public string? Carrier {  get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set;}
        public DateTime? DateOfPayement { get; set; }
        public DateTime? DueDate {  get; set; }
        
        public int phone {  get; set; }
        public string Address {  get; set; }
        public string City { get; set; }

        public string state {  get; set; }
    }
}
