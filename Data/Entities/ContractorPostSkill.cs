namespace Data.Entities
{
    public class ContractorPostSkill
    {
        public int ContractorPostID { get; set; }

        public int SkillID { get; set; }

        public ContractorPost ContractorPost { get; set; }

        public Skill Skills { get; set; }
    }
}