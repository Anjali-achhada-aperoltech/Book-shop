using ROMS.Domain;
using System.Text.Json.Serialization;

namespace Book.Domain.Models
{
    public class WishList:BaseEntity
    {
        public Guid? BookitemId { get; set; }
        [JsonIgnore]
        public BookItems BookItem { get; set; }

        public string? ApplicationuserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
