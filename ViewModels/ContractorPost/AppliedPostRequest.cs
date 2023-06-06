using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Quizzes;

namespace ViewModels.ContractorPost
{
    public class AppliedPostRequest
    {
        public int PostId { get; set; }   
        public decimal? WishSalary { get; set; }
        public string? Video { get; set; }
        public List<AppliedGroup>? GroupMember { get; set; }
        public bool IsGroup { get; set; } = false;
        public QuizSubmit? QuizSubmit { get; set; }
    }

    public class AppliedGroup
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public Guid TypeID { get; set; }
        public string? TypeName { get; set; }
        public string VerifyId { get; set; }

        public string? SkillAssessment { get; set; }

        public int? BehaviourAssessment { get; set; }
    }
}
