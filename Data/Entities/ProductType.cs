using Data.Enum;

namespace Data.Entities
{
    public class ProductType
    {

        public int Id { get; set; }
        //public string Name { get; set; }

        public Products Products { get; set; }

        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public int? ColorId { get; set; }
        public Color? Color { get; set; }

        public int? SizeID { get; set; }
        public ProductSize? Size { get; set; }

        public string? Label { get; set; }

        public int? OtherID { get; set; }
        public Other? Other { get; set; }

        public Status Status { get; set; }



        public List<BillDetail>? BillDetails { get; set; }




        public List<Cart>? Carts { get; set; }
    }
}
