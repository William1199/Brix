using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MaterialStore
{
    public class ReportRequestDTO
    {
        public int? ProductId { get; set; }
        public int? ContractorPostId { get; set; }
        public string ReportProblem { get; set; }
    }
}
