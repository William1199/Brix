using Data.Enum;
using ViewModels.ContractorPost;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.ContractorPosts
{
    public interface IContractorPostService
    {
        Task<BasePagination<List<ContractorPostDTO>>> GetPost(PaginationFilter filter, Guid id);

        Task<BasePagination<List<ContractorPostDTO>>> GetPostByUserId(PaginationFilter filter, Guid id);

        Task<BasePagination<List<ContractorPostDTO>>> GetPostByCategories(PaginationFilter filter, Guid id,PostCategories categories);

        Task<BasePagination<List<ContractorPostDTO>>> GetPostByContractor(PaginationFilter filter, Guid id);

        Task<BasePagination<List<ContractorPostDTO>>> GetPostByViews(PaginationFilter filter);

        Task<BasePagination<List<ContractorPostDTO>>> SearchPost(PaginationFilter filter, string keyword);

        Task<BaseResponse<string>> AppliedPost(AppliedPostRequest request, Guid userID);
        Task<BaseResponse<string>> UpdatePost(ContractorPostUpdate request);

        Task<BasePagination<List<AppliedPostDTO>>> ViewAppliedPost(int postID, PaginationFilter filter);

        Task<bool> CreateContractorPost(ContractorPostModels contractorPostDTO);

        Task<BaseResponse<ContractorPostDetailDTO>> GetDetailPost(int cPostid, int? pageSize);

        Task<BasePagination<List<AppliedPostAll>>> ViewAllPostApplied(Guid userID, PaginationFilter request);

        Task<BaseResponse<QuizSubmitDetailDTO>> ViewDetailQuizSubmit(int quizId,int builderId);

        Task<BaseResponse<bool>> ViewPostAppliedCheck(int builderId,Guid contractorId);

        Task<BaseResponse<string>> UpdatePostStatus(int postId);
    }
}
