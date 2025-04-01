using Book.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class CartVm : BaseDTO
    {
        public IEnumerable<Cart> Carts { get; set; }
        public IEnumerable<CartItemDto> CartDetails { get; set; }
        public double Total { get; set; }
        public OrderHeader OrderHeader { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]

        public int phone { get; set; }
        [Required(ErrorMessage = "Adddress is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required.")]

        public string City { get; set; }
        [Required(ErrorMessage = "State is required.")]

        public string state { get; set; }
    }
    public class CartItemDto
    {
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public int? price { get; set; }
        public int? Total { get; set; }
    }
}
