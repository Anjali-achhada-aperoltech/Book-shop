using ROMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Book.Domain.Models
{
    public class Cart:BaseEntity

    {
        public int? quantity {  get; set; }
        public Guid? BookitemId { get; set; }
        [JsonIgnore]
        public BookItems  BookItem { get; set; }
        public string? ApplicationuserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }



    }
}
