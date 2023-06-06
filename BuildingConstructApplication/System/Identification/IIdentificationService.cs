using Data.Enum;
using Emgu.CV;
using ViewModels.Identification;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.Identification
{
    public interface IIdentificationService
    {

        Task<BaseResponse<IdentificationDTO>> GetDetail(int id);

        Task<BasePagination<List<IdentificationDTO>>> GetAll(PaginationFilter filter);

        Task<BaseResponse<string>> Create(Guid userID, CreateIndetificationRequest requests);

        Task<BaseResponse<UserIdentification>> Update(int id, Status status);

        public BaseResponse<string> DetectFace(Mat image);


    }
}
