using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class WorkerContructionType
    {
        public int ConstructionTypeId { get; set; }
        public int BuilderId { get; set; }

        public ConstructionType ConstructionType { get; set; }
        public Builder Builder { get; set; }
     }
}
