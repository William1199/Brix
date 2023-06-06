namespace Data.Entities
{
    public class GroupMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public Guid TypeID { get; set; }
        public Type Type { get; set; }

        public string IdNumber { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        public string? SkillAssessment { get; set; }

        public int? BehaviourAssessment { get; set; }

    }
}
