using Data.Enum;

namespace ViewModels.ContractorPost
{
    public class ContractorPostModels
    {
        public string Title { get; set; } = string.Empty;

        public string ProjectName { get; set; } = string.Empty;
        public List<TypeModels>? type { get; set; }

        public string Description { get; set; } = string.Empty;
        public string? Benefit { get; set; }
        public string? Required { get; set; }

        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        public Place Place { get; set; }
        public PostCategories PostCategories { get; set; }

        public string Salaries { get; set; } = string.Empty;

        public int NumberPeople { get; set; }

        public string? ConstructionType { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public bool Accommodation { get; set; }
         
        public bool Transport { get; set; }

        public bool QuizRequired { get; set; }

        public bool VideoRequired { get; set; }


    }
    public class ContractorPostUpdate
    {
        public int? PostId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string ProjectName { get; set; } = string.Empty;
        public List<TypeModels>? type { get; set; }

        public string Description { get; set; } = string.Empty;
        public string? Benefit { get; set; }
        public string? Required { get; set; }

        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        public Place Place { get; set; }
        public PostCategories PostCategories { get; set; }

        public string Salaries { get; set; } = string.Empty;

        public int NumberPeople { get; set; }

        public string? ConstructionType { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public bool Accommodation { get; set; }

        public bool Transport { get; set; }

        public bool QuizRequired { get; set; }

        public bool VideoRequired { get; set; }

    }
}
