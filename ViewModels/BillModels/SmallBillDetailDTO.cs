using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BillModels
{
    public class SmallBillDetailDTO
    {
        public BigBillDetail? Bill { get; set; }
        public StoreDTO? Store { get; set; }
        public BuyerInfo? BuyerInfo { get; set; }
        public List<SmallBillDTO> Details { get; set; }

    }


    public class SmallBillDTO
    {
        public Status Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; }

        public List<ProductBillDetail>? ProductBillDetail { get; set; }
    }

    public class StoreDTO
    {
        public int? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? StoreName { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
    }


    public class BuyerInfo
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

    }
}
