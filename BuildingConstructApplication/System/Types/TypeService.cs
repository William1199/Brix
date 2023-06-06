using Data.DataContext;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ContractorPost;
using ViewModels.Response;
using ViewModels.Types;

namespace Application.System.Types
{
    public class TypeService : ITypeService
    {
        private readonly BuildingConstructDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public TypeService(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<BaseResponse<string>> CreateType(TypeRequest type)
        {
            BaseResponse<string> response = new();
            var types = new Data.Entities.Type()
            {
                Id = new Guid(),
                Name = type.Name
            };
            var check = await _context.Types.Where(x => x.Id.ToString().Equals(types.Id.ToString())).FirstOrDefaultAsync();
            var checkDuplicateName = await _context.Types.Where(x => x.Name.Equals(types.Name)).CountAsync();
            if (check == null && checkDuplicateName==0)
            {

                await _context.Types.AddAsync(types);
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
            return response;
        }

        public async Task<BaseResponse<string>> DeleteType(string typeId)
        {
            BaseResponse<string> response = new();
            var check = await _context.Types.Include(x=>x.Skill).Include(x=>x.Builder).Include(x=>x.ContractorPostTypes).Where(x => x.Id.ToString().Equals(typeId)).FirstOrDefaultAsync();
            if (check != null)
            {
                if (!check.Skill.Any() && !check.Builder.Any() && !check.ContractorPostTypes.Any())
                {
                    _context.Types.Remove(check);
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

        public async Task<BaseResponse<List<TypeModels>>> GetAllTypeAndSkills()
        {
            BaseResponse<List<TypeModels>> response = new();
            response.Data = new();
            var lType = new List<TypeModels>();
            var rs = await _context.Types.Include(x => x.Skill).ToListAsync();
            if (rs != null)
            {
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
                foreach (var item in rs)
                {
                    var type = new TypeModels();
                    type.id = item.Id;
                    type.name = item.Name;
                    var lSkillArr = new List<SkillArr>();
                    foreach (var i in item.Skill)
                    {
                        var skill = new SkillArr();
                        skill.id = i.Id;
                        skill.name = i.Name;
                        skill.fromSystem = i.FromSystem;
                        skill.TypeId = i.TypeId;
                        lSkillArr.Add(skill);
                    }
                    lType.Add(type);
                    type.SkillArr = lSkillArr;
                }
                response.Data = lType;

                return response;
            }
            response.Code = BaseCode.ERROR;
            response.Message = BaseCode.ERROR_MESSAGE;
            return response;
        }

        public async Task<BaseResponse<string>> UpdateType(TypeRequest type)
        {
            BaseResponse<string> response = new();
            var check = await _context.Types.Where(x => x.Id.ToString().Equals(type.typeId)).FirstOrDefaultAsync();
            if (check != null)
            {
                check.Name = type.Name;
                _context.Types.Update(check);
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
