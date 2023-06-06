using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MaterialStore
{
    public class ProductDTO
    {
        public string? Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitInStock { get; set; }

        public string? Image { get; set; }
        public string Unit { get; set; }

        public string? Description { get; set; }
        public int? SoldQuantities { get; set; }


        public string? Brand { get; set; }

        public List<CategoryProductDTO>? Categories { get; set; }

        public List<ProductTypeDTO>? ProductTypes { get; set; }
    }
}
