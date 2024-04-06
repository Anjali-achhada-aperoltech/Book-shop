using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class LoginDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Please enter email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]


        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe {  get; set; }
    }
}
