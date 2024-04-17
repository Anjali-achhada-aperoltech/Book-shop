using Microsoft.EntityFrameworkCore.Storage.Json;
using ROMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Models
{
    public class OrderDetail:BaseEntity
    {
        public Guid OrderHeaderId {  get; set; }
        public OrderHeader OrderHeader { get; set; }
        public Guid BookItemId {  get; set; }
        public BookItems BookItem{ get; set; }
        public int price {  get; set; }
        public int quantity {  get; set; }

        
    }
}
