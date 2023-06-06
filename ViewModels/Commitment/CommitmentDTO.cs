using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Commitment
{
    public class CommitmentDTO
    {
        public int? Id { get; set; }

        public string? OptionalTerm { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }

        public string? Title { get; set; }

        public string? ProjectName { get; set; }
        public int? PostID { get; set; }

        public string? BuilderName { get; set; }

        public string? ConstructorName { get; set; }

        public string? Salary { get; set; }

        public string? BuilderTypeName { get; set; }

        public string? BuilderPhone { get; set; }

        public string? BuilderAvatar { get; set; }

        public string? ConstructorAvatar { get; set; }

        public List<CommitmentGroup>? Groups { get; set; }

    }
}
