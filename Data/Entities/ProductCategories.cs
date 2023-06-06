using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ProductCategories
    {
        public int CategoriesID { get; set; }

        public int ProductID { get; set; }

        public Products Products { get; set; }

        public Categories Categories { get; set; }

        public string? Name { get; set; }
    }
}
