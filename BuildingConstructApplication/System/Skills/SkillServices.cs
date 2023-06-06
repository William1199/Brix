using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ViewModels.Pagination;
using ViewModels.Response;
using ViewModels.Skills;

namespace Application.System.Skills
{
    public class SkillServices : ISkillService
    {
        private readonly BuildingConstructDbContext _context;

        public SkillServices(BuildingConstructDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<string>> CreateSkill(SkillRequest skill)
        {
            BaseResponse<string> response = new();
            var skills = new Data.Entities.Skill()
            {
                Name = skill.Name,
                FromSystem=true,
                TypeId=Guid.Parse(skill.TypeId)
            };
            var check = await _context.Skills.Where(x => x.Id==skills.Id).FirstOrDefaultAsync();
            if (check == null)
            {
                await _context.Skills.AddAsync(skills);
                var rs = await _context.SaveChangesAsync();
                if (rs > 0)
                {
                    response.Code = BaseCode.SUCCESS;
                    response.Message = BaseCode.SUCCESS_MESSAGE;
                }
                else
                {

                    response.Code = BaseCode.ERROR;
                    response.Message = BaseCode.ERROR_MESSAGE;
                }
            }
            return response;
        }

        public async Task<BaseResponse<string>> DeleteSkill(int skillID)
        {
            BaseResponse<string> response = new();
            var check = await _context.Skills.Include(x => x.BuilderSkills).Include(x => x.ContractorPostSkills).Where(x => x.Id==skillID).FirstOrDefaultAsync();
            if (check != null)
            {
                if (!check.BuilderSkills.Any() && !check.ContractorPostSkills.Any())
                {
                    _context.Skills.Remove(check);
                    var rs = await _context.SaveChangesAsync();
                    if (rs > 0)
                    {
                        response.Code = BaseCode.SUCCESS;
                        response.Message = BaseCode.SUCCESS_MESSAGE;
                    }
                    else
                    {
                        response.Code = BaseCode.ERROR;
                        response.Message = BaseCode.ERROR_MESSAGE;
                    }
                }
                else
                {
                    response.Code = BaseCode.ERROR;
                    response.Message = BaseCode.ERROR_MESSAGE;
                }
            }
            return response;
        }

        public async Task<BasePagination<List<Skill>>> GetAll(PaginationFilter filter)
        {
            BasePagination<List<Skill>> response;
            var orderBy = filter._orderBy.ToString();
            int totalRecord;
            orderBy = orderBy switch
            {
                "1" => "ascending",
                "-1" => "descending",
                _ => orderBy
            };

            if (string.IsNullOrEmpty(filter._sortBy))
            {
                filter._sortBy = "Id";
            }

            var result = await _context.Skills
                .AsNoTracking()
                     .Where(x => x.FromSystem == true)
                     .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .ToListAsync();


         
                totalRecord = await _context.Skills.Where(x=>x.FromSystem==true).CountAsync();

            if (!result.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new(),
                    Pagination = null
                };
            }
            else
            {
                double totalPages;

                totalPages = ((double)totalRecord / (double)filter.PageSize);

                var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                Pagination pagination = new()
                {
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalPages = roundedTotalPages,
                    TotalRecords = totalRecord
                };

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = result,
                    Pagination = pagination
                };
            }
            return response;
        }

        public async Task<BaseResponse<string>> UpdateSkill(SkillRequest skill)
        {
            BaseResponse<string> response = new();
            var check = await _context.Skills.Where(x => x.Id==skill.skillId).FirstOrDefaultAsync();
            if (check != null)
            {
                check.Name = skill.Name;
                if (skill.TypeId != null)
                {
                    check.TypeId = Guid.Parse(skill.TypeId);
                }
                _context.Skills.Update(check);
                var rs = await _context.SaveChangesAsync();
                if (rs > 0)
                {
                    response.Code = BaseCode.SUCCESS;
                    response.Message = BaseCode.SUCCESS_MESSAGE;
                }
                else
                {
                    response.Code = BaseCode.ERROR;
                    response.Message = BaseCode.ERROR_MESSAGE;
                }
            }
            return response;
        }
    }
}
