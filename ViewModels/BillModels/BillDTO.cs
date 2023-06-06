using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BillModels
{
    public class BillDTO
    {
        public string? Notes { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? BillId { get; set; }
        public string? StoreName { get; set; }
        public string? Avatar { get; set; }
        public string? Reason { get; set; }
        public int? StoreID { get; set; }
        public string? UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public int? ProductTypeId { get; set; }
        public List<ProductBillDTO>? ProductBillDetail { get; set; } 
        public List<ProductBillDetail>? ProductBillDetailGet { get; set; } 

    }
}
