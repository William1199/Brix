using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ContractorPost
{
    public class SkillArr
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool fromSystem { get; set; }
        public Guid? TypeId { get; set; }
    }
}
