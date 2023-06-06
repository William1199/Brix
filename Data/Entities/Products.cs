using Data.Enum;

namespace Data.Entities
{
    public class Products:BaseEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Unit { get; set; }
        public decimal UnitPrice { get; set; }

        public int UnitInStock { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public int SoldQuantities { get; set; }

        public int? MaterialStoreID { get; set; }

        public MaterialStore MaterialStore { get; set; }

        public List<ProductCategories>? ProductCategories { get; set; }

        public List<Cart> Carts { get; set; }

        public List<BillDetail>? BillDetails { get; set; }

        public List<ProductType>? ProductTypes { get; set; }
        public List<Report>? Reports { get; set; }


        public bool Status { get; set; }

        
    }
}
