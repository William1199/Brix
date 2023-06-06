using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Payment
{
    public class PaymentRequestDTO
    {
        public string UserId { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string VnPayResponseCode { get; set; }
        public string Amount { get; set; }
    }
}
