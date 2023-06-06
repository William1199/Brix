namespace Data.Entities
{
    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool FromSystem { get; set; }
        public Guid? TypeId { get; set; }
        public Type Type { get; set; }
        public List<BuilderSkill>? BuilderSkills { get; set; }
        public List<ContractorPostSkill>? ContractorPostSkills { get; set; }
    }
}