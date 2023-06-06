using Data.Enum;
using System.Security.Cryptography.Xml;

namespace Data.Entities
{
    public class ContractorPost : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string ProjectName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string? Benefit { get; set; }
        public string? Required { get; set; }

        public List<ContractorPostSkill>? PostSkills { get; set; }
        public List<ContractorPostType>? ContractorPostTypes { get; set; }

        public List<PostCommitment>? PostCommitments { get; set; }
        public List<Report>? Reports { get; set; }
        public List<AppliedPost>? AppliedPosts { get; set; }
        public List<Quiz>? Quizzes { get; set; }
        public List<PostInvite>? PostInvites { get; set; }
        public List<Save>? Saves { get; set; }


        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }

        public Status Status { get; set; }    

        public Place Place { get; set; }

        public PostCategories PostCategories { get; set; }

        public string Salaries { get; set; } = string.Empty;

        public int ViewCount { get; set; }

        public int NumberPeople { get; set; }

        public int PeopeRemained { get; set; }
        public bool? isApplied { get; set; }

        public bool QuizRequired { get; set; }

        public bool VideoRequired { get; set; }

        public string? ConstructionType { get; set; }
        
        public string? StartTime { get; set; }

        public string? EndTime { get; set; }    

        public bool Accommodation { get; set; }

        public bool Transport { get; set; }

        public int ContractorID { get; set; }

        public Contractor? Contractor { get; set; }

    }
}