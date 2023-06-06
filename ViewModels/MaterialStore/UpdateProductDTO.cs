namespace ViewModels.MaterialStore
{
    public class UpdateProductDTO
    {
        public int productId { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitInStock { get; set; }

        public string? Image { get; set; }
        public string Unit { get; set; }

        public string? Description { get; set; }
        public int? SoldQuantities { get; set; }


        public string? Brand { get; set; }

        public List<CategoryProductDTO>? Categories { get; set; }
        public List<UpdateProductType>? ProductTypes { get; set; }
    }
}
