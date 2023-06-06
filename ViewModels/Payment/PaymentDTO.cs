using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Payment
{
    public class PaymentDTO
    {
        public string FullName { get; set; }
        public string Phonenumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ExpireationDate { get; set; }
        public string Price { get; set; }
        public string? TransactionId { get; set; }
        public string? PaymentId { get; set; }
        public bool IsRefund { get; set; }
        public string VnPayResponseCode { get; set; }
        public DateTime? ExtendDate { get; set; }
    }
}
