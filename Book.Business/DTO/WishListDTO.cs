using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class WishListDTO
    {
        public List<WishListItemDTO> WishlistItems { get; set; }
    }

    public class WishListItemDTO
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public string BookImage { get; set; }
    }

}
