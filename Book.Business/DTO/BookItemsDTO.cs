using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class BookItemsDTO:BaseDTO
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Book Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Book Description")]

        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Required]
       
        [Display(Name = "Book Price")]

        public int price { get; set; }
        [Required]

        public Guid CategoryId { get; set; }
        [Required]

        public string FrontImage { get; set; }
        [Required]

        public Guid BookLanguageId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
