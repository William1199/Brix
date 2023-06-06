using Data.Entities;
using Data.Enum;
using ViewModels.MaterialStore;
using ViewModels.Users;

namespace ViewModels.ContractorPost
{
    public class ContractorPostDetailDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string ProjectName { get; set; }
        public List<TypeModels> type { get; set; }

        public string? Description { get; set; }
        public string? Benefit { get; set; }
        public string? Required { get; set; }

        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        public Place Place { get; set; }

        public PostCategories PostCategories { get; set; }

        public string Salaries { get; set; }

        public int NumberPeople { get; set; }
        public int PeopleRemained { get; set; }
        public bool? IsApplied { get; set; }
        public bool? IsSave { get; set; }

        public bool? IsGroup { get; set; }

        public int? QuizId { get; set; }

        public bool IsQuizAnswer { get; set; }

        public DateTime LastModifiedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public UserModelsDTO Author { get; set; }

        public string? ConstructionType { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public bool Accommodation { get; set; }
        public int ViewCount { get; set; }

        public bool Transport { get; set; }

        public Status Status { get; set; }

        public bool RequiredQuiz { get; set; }

        public bool VideoRequired { get; set; }

        public List<Quiz>? Quizzes { get; set; }

        public List<MaterialStoreDTO>? RecommendStore { get; set; }

    }


   

}
