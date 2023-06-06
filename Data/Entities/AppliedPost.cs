using Data.Enum;

namespace Data.Entities
{
    public class AppliedPost
    {

        public int PostID { get; set; }

        public int BuilderID { get; set; }

        public int? GroupID { get; set; }

        public Group Group { get; set; }

        public ContractorPost ContractorPosts { get; set; }

        public Builder Builder { get; set; }

        public Status Status { get; set; }

        public DateTime AppliedDate { get; set; }

        public decimal? WishSalary { get; set; }

        public int? QuizId { get; set; }

        public Quiz? Quiz { get; set; }

        public string? Video { get; set; }



    }
}
