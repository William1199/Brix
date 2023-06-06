namespace ViewModels.ContractorPost
{
    public class AppliedPostDTO
    {
        public Guid? UserID { get; set; }

        public int BuilderID { get; set; }

        public string? FirstName { get; set; }

        public string? Avatar { get; set; }

        public string? TypeName { get; set; }

        public string? LastName { get; set; }

        public decimal? WishSalary { get; set; }

        public DateTime? AppliedDate { get; set; }

        public List<AppliedGroup>? Groups { get; set; }

        public int? QuizId { get; set; }

        public string? Video { get; set; }

        public string? QuizName { get; set; }

        public int? TotalNumberQuestion { get; set; }

        public int? TotalCorrectAnswers { get; set; }

    }
}
