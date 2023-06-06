using Data.Enum;
using ViewModels.Commitment;
using ViewModels.Quizzes;

namespace ViewModels.ContractorPost
{
    public class AppliedPostAll
    {
        public int? PostId { get; set; }

        public string? Avatar { get; set; }

        public string? Title { get; set; }

        public string? ProjectName { get; set; }

        public string? Description { get; set; }

        public string? AuthorName { get; set; }

        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        public Place Place { get; set; }

        public DateTime AppliedDate { get; set; }

        public decimal? WishSalary { get; set; }

        public string? Video { get; set; }

        public Status Status { get; set; }  

        public bool? IsSave { get; set; }

        public List<CommitmentGroup>? Groups { get; set; }
        public int? QuizId { get; set; }
        public string? QuizName { get; set; }
        public Guid? TypeId { get; set; }
        public List<QuizQuestionDTO>? Questions { get; set; }

    }

    public class QuizQuestionDTO
    {
        public int QuestionId { get; set; }

        public string QuestionName { get; set; } = string.Empty;

        public List<QuizAnswerDTO>? Answers { get; set; }

    }

    public class QuizAnswerDTO
    {
        public int AnswerId { get; set; }

        public string AnswerName { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int Answer { get; set; }
    }

}
