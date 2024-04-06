using Book_Shop.Models;
using ROMS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Models
{
    public class BookItems:BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Display(Name="Book Name")]
        public string Name {  get; set; }
        [Required]
        [Display(Name = "Book Description")]

        public string Description {  get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Book Price")]


        public int price {  get; set; }
        [Required]

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]

        public string ImageUrl {  get; set; }
        public DateTime CreatedDate { get; set;}=DateTime.Now;
        public DateTime UpdatedDate { get; set;} =DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
