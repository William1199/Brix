using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ContractorPost;
using ViewModels.Pagination;
using ViewModels.Response;
using ViewModels.Skills;

namespace Application.System.Skills
{
    public interface ISkillService
    {
        Task<BasePagination<List<Skill>>> GetAll(PaginationFilter filter);
        Task<BaseResponse<string>> CreateSkill(SkillRequest skill);
        Task<BaseResponse<string>> DeleteSkill(int skillID);
        Task<BaseResponse<string>> UpdateSkill(SkillRequest skill);
    }
}
