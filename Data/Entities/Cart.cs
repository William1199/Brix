using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Entities
{
    public class Cart
    {
        public int Id { get; set; } 

        public Guid UserID { get; set; }

        public int ProductID { get; set; }

        public int? TypeID { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public Products Products { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public ProductType? ProductType { get; set; }

        public DateTime LastModifiedAt { get; set; } = DateTime.Now;

    }
}
