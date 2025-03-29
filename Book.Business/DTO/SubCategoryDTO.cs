
using Book_Shop.Models;
using System.ComponentModel.DataAnnotations;

namespace Book.Business.DTO
{
    public class SubCategoryDTO:BaseDTO
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
