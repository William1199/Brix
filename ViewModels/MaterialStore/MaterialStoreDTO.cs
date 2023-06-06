using Data.Enum;

namespace ViewModels.MaterialStore
{
    public class MaterialStoreDTO
    {

        public string? FirstName { get; set; }  
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public Guid? UserId { get; set; }

        public int Id { get; set; }

        public string? Webstie { get; set; }

        public string? Description { get; set; }

        public string? TaxCode { get; set; }

        public string? Experience { get; set; }

        public string? Image { get; set; }

        public Place? Place { get; set; }
    }
}