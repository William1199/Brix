using Data.Enum;

namespace ViewModels.Commitment
{
    public class DetailCommitmentDTO
    {

        public CommitmentUser? PartyA { get; set; }

        public CommitmentUser? PartyB { get; set; }

        public List<CommitmentGroup>? Group { get; set; }
        public string? Title { get; set; }
        public int? Id { get; set; }

        public string? ProjectName { get; set; }
        public int? PostID { get; set; }

        public string? Salaries { get; set; }

        public string? Description { get; set; }
        public string? OptionalTerm { get; set; }

        public string? PostSalaries { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAccepted { get; set; }

    }




}
