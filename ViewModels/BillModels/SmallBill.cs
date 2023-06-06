using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BillModels
{
    public class SmallBill
    {
        public string? Notes { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductBillDTO>? SmallProductDetail { get; set; }

    }
}
