using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class RegisterDTO
    {

        

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [DataType(DataType.Date)]

        public DateTime? DateOfBirth { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       public string ConfrimPassword {  get; set; }

    }
}
