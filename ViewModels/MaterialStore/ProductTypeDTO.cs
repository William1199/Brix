using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MaterialStore
{
    public class ProductTypeDTO
    {
        public int? Id { get; set; }
        public string? TypeName { get; set; }

        public string? Label { get; set; }

        public int Quantity { get; set; }


        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public int? OtherId { get; set; }

        public string? Color { get; set; }
        public string? Other { get; set; }
        public string? Size { get; set; }

        public string? Image { get; set; }

        //public string? OtherImage { get; set; }

    }
}
