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
        [Required(ErrorMessage="Please enter email")]

        public string Email {  get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]


        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirm password can not match")]

        public string ConfrimPassword {  get; set; }
    }
}
