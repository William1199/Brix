    using Data.Enum;

namespace Data.Entities
{
    public class Builder:BaseEntity
    {
        public int Id { get; set; }

        public string? ExperienceDetail { get; set; }

        public string? Certificate { get; set; }

        public int? Experience { get; set; }
        public string? Image { get; set; }

        public Place? Place { get; set; }
        public string? ConstructionType { get; set; }
        public List<BuilderSkill>? BuilderSkills { get; set; }

        public User? User { get; set; }

        public Guid? TypeID { get; set; }

        public Type? Type { get; set; }
        
        public List<Group>? Groups { get; set; }

        public List<AppliedPost>? AppliedPosts { get; set; }

        public List<PostCommitment>? PostCommitments { get; set; }
        public List<WorkerContructionType>? WorkerContructionTypes { get; set; }
        public List<PostInvite>? PostInvites { get; set; }
        public List<UserAnswer>? UserAnswers { get; set; }


    }
}