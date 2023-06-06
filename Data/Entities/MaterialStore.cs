using Data.Enum;

namespace Data.Entities
{
    public class MaterialStore:BaseEntity
    {
        public int Id { get; set; }

        public string? Website { get; set; }

        public string? Description { get; set; }

        public string? TaxCode { get; set; }

        public string? Experience { get; set; }

        public string? Image { get; set; }

        public Place? Place { get; set; }    

        public User? User { get; set; }

        public List<Products> Products { get; set; }

        public List<Bill>? Bills { get; set; }


    }
}