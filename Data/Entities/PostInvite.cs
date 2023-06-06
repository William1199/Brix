using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PostInvite
    {
        public int Id { get; set; }

        public Contractor? Contractor { get; set; }

        public int? ContractorId { get; set; }

        public Builder? Builder { get; set; }

        public int? BuilderId { get; set; }

        public ContractorPost? ContractorPost { get; set; }  

        public int? ContractorPostId { get; set; }

        public DateTime LastModifiedAt { get; set; }= DateTime.Now;

        public bool IsRead { get; set; }
    }
}
