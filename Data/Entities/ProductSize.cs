using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LastModifiedAt { get; set; } = DateTime.Now;

        public List<ProductType>? ProductTypes { get; set; }

    }
}
