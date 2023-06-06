using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BillModels
{
    public class UpdateBillRequest
    {
        public Status Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
