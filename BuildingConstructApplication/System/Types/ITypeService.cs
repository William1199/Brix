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
    public interface ITypeService
    { 
        Task<BaseResponse<List<TypeModels>>> GetAllTypeAndSkills();
        Task<BaseResponse<string>> CreateType(TypeRequest type);
        Task<BaseResponse<string>> DeleteType(string typeId);
        Task<BaseResponse<string>> UpdateType(TypeRequest type);  
    }
}
