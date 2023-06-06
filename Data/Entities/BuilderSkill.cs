namespace Data.Entities
{
    public class BuilderSkill
    {
        public Builder Builder { get; set; }
        public Skill Skill { get; set; }

        public int BuilderSkillID { get; set; }

        public int SkillID { get; set; }
    }
}