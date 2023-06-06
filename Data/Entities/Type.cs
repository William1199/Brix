using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Type
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Builder>? Builder { get; set; }
        public List<Skill> Skill { get; set; }
        public List<ContractorPostType>? ContractorPostTypes { get; set; }
        public List<Quiz>? Quiz { get; set; }

    }
}
