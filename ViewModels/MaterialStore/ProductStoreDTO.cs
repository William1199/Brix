using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Categories;

namespace ViewModels.MaterialStore
{
    public class ProductStoreDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? StoreName { get; set; }
        public int? StoreID { get; set; }
        public string? StoreImage { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitInStock { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public int SoldQuantities { get; set; }
        public bool? isAll { get; set; }
        public List<CategoryDTO>? ProductCategories { get; set; }
        public DateTime LastModifiedAt { get; set; }

    }
}
