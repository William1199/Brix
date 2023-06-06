namespace ViewModels.Quizzes
{
    public class QuizDTO
    {
        public int QuizId { get; set; }

        public string? QuizName { get; set; }

        public int? TotalQuestion { get; set; }

        public bool HasAlreadyAttemped { get; set; }

        public List<QuestionDTO>? Questions { get; set; }
    }


    public class QuestionDTO
    {
        public int QuestionId { get; set; }

        public string? QuestionName { get; set; }

        public List<AnswerDTO>? Answers { get; set; } = new();


    }

    public class AnswerDTO
    {
        public int AnswerId { get; set; }

        public string? AnswerName { get; set; }

        public bool IsCorrect { get; set; }
    }

}
