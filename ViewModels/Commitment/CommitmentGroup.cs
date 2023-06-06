using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Commitment
{
    public class CommitmentGroup
    {
        public string Name { get; set; } 

        public DateTime DOB { get; set; }

        public Guid TypeID { get; set; }

        public string? TypeName { get; set; }

        public string VerifyId { get; set; }
    }
}
