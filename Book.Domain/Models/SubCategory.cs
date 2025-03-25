using Book_Shop.Models;
using ROMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace Book.Domain.Models;
public class SubCategory:BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    [StringLength(50)]
    public string Description { get; set; }
    public Guid CategoryId { get; set; }  
    public Category Category { get; set; }     
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
    [Required]
    public string CreatedBy { get; set; }

    [Required]
    public string ModifiedBy { get; set; }

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
