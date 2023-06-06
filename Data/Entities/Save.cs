using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Save
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? ContractorPostId { get; set; }
        public User User { get; set; }
        public ContractorPost? ContractorPost { get; set; }

        public DateTime Date { get; set; }

    }
}
