using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Categories
{
    public class CategoryDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public Data.Enum.TypeEnum Type { get; set; }
    }
}
