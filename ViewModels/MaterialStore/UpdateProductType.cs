using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MaterialStore
{
    public class UpdateProductType
    {
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Other { get; set; }
        public int Quantity { get; set; }

        public string? Image { get; set; }

        public string? Label { get; set; }
    }
}
