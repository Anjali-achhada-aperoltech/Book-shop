using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class CreateBookDTO:BaseDTO
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
        [Display(Name = "Book Price")]
        public string AuthorName {  get; set; }

        public int price { get; set; }
        [Required]

        public string CategoryName { get; set; }
        [Required]
      public string BookLanguageName { get; set; }

        public string FrontImage { get; set; }
    }
}
