using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class ApplicationUserDTO
    {
        public string id {  get; set; }
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }  
    }
}
