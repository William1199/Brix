namespace ViewModels.ContractorPost
{
    public class QuizSubmitDetailDTO
    {
        public int? QuizId { get; set; }
        public string? QuizName { get; set; }
        public Guid? TypeId { get; set; }
        public List<QuizQuestionDTO>? Questions { get; set; }

    }
}
