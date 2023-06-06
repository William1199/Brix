using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Bill:BaseEntity
    {
        public int Id { get; set; }

        public string? Note { get; set; }

        public DateTime? PaymentDate { get; set; }
        //public BillType? Type { get; set; }

        public string? Reason { get; set; }

        public Status Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public int? ContractorId { get; set; }

        public Contractor? Contractor { get; set; }

        public int? StoreID { get; set; }

        public MaterialStore? MaterialStore { get; set; }

        public List<BillDetail>? BillDetails { get; set; }

    }
}
