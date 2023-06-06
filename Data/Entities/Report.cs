using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Report:BaseEntity
    {
        public int Id { get; set; }
        public int? ContractorPostId { get; set; }
        public int? ProductId { get; set; }
        public ContractorPost ContractorPosts { get; set; }
        public Products Products { get; set; }
        public string ReportProblem { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
