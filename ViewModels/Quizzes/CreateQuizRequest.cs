using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Quizzes
{
    public class CreateQuizRequest
    {
        public string? QuizName { get; set; }
        public Guid? TypeId { get; set; }
        public int PostId { get; set; }
        public List<CreateQuestionDTO>? Questions { get; set; }
    }

    public class CreateQuestionDTO
    {
        public string QuestionName { get; set; } = string.Empty;

        public List<CreateAnswerDTO>? Answers { get; set; }

    }

    public class CreateAnswerDTO
    {
        public string AnswerName { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}
