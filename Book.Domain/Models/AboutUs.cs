using ROMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace Book.Domain.Models
{
    public class AboutUs:BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string PageTitle {  get; set; }
        [Required]
        [StringLength(50)]
        public string PageDescription {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
