using ROMS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Models
{
    public class ContactUs:BaseEntity
    {
       
            [Required, StringLength(100)]
            public string Name { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Subject { get; set; }

            [Required, StringLength(500)]
            public string Message { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime ModifiedAt { get; set; } = DateTime.Now;
            public string? CreatedBy { get; set; }
            public string? ModifiedBy { get; set; }
            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
        
    }
}
