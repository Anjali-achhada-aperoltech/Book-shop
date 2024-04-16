using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Models
{
    public class Applicationuser:IdentityUser
    {
        [Required]
        public string? FirstName {  get; set; }
        [Required]
        public string? LastName { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateOfBirth {  get; set; }
      
        public DateTime? CreatedOn { get; set; }=DateTime.Now;
        public DateTime? UpdatedOn { get;set; }=DateTime.Now;
        public bool? IsActive {  get; set; }
        public bool? IsDeleted {  get; set; }
    }
}
