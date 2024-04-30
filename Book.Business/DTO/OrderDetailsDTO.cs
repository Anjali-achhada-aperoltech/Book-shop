using Book.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class OrderDetailsDTO:BaseDTO
    {
        public Guid BookItemId {  get; set; }
        public IEnumerable<Cart> Carts { get; set; }
        public string Applicationuserid {  get; set; }
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }

        public string? email {  get; set; }
        public string Address {  get; set; }
        public string city {  get; set; }
        public string state { get; set; }
        public int phoneno { get; set; }
        public string OrderStatus {  get; set; }
        public double OrderTotal {  get; set; }
        public DateTime OrderDate { get; set; }
        public string productname { get; set; }
        public double quantity { get; set; }
        public int price { get; set; }



    }
}
