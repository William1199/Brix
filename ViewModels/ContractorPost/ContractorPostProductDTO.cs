using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ContractorPost
{
    public class ContractorPostProductDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int? Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitInStock { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public Data.Entities.Categories? Categories { get; set; }
    }
}
