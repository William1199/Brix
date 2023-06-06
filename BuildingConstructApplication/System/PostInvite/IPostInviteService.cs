using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Identification;
using ViewModels.Pagination;
using ViewModels.PostInvite;
using ViewModels.Response;

namespace Application.System.PostInvite
{
    public interface IPostInviteService
    {
        Task<BasePagination<List<PostInviteDTO>>> GetAll(PaginationFilter filter,Guid UserID);

        Task<BaseResponse<bool>> isInvite(int builderID, int postID);

        Task<BaseResponse<List<PostInviteDTO>>> CheckInvite(int builderID, Guid contractorId);

        Task<BaseResponse<string>> Create(CreatePostIniviteRequest requests);

        Task<BaseResponse<string>> Update(int id);

    }
}
