using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ContractorPostType
    {
        public int ContractorPostID { get; set; }

        public Guid TypeID { get; set; }

        public ContractorPost ContractorPost { get; set; }

        public Type Type { get; set; }
    }
}
