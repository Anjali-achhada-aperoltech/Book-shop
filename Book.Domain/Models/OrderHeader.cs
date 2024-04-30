using ROMS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Phone]

        public int phone {  get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3,
        ErrorMessage = "Enter Valid Address")]

        public string Address {  get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Enter City")]
        public string City { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Enter State")]


        public string state {  get; set; }
    }
}
