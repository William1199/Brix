using Application.System.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using ViewModels.Pagination;
using ViewModels.Skills;

namespace BuildingConstructApi.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter request)
        {
            dynamic validFilter;

            if (request.FilterRequest == null)
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            }
            else
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy, request.FilterRequest);

            }

            var result = await _skillService.GetAll(validFilter);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSkill(SkillRequest skill)
        {
            var rs = await _skillService.CreateSkill(skill);
            return Ok(rs);
        }
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteSkill(int skillId)
        {
            var rs = await _skillService.DeleteSkill(skillId);
            return Ok(rs);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSkill([FromBody] SkillRequest skill)
        {
            var rs = await _skillService.UpdateSkill(skill);
            return Ok(rs);
        }

    }
}
