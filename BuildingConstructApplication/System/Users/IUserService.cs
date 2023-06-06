using Application.ClaimTokens;
using ViewModels.Pagination;
using ViewModels.Response;
using ViewModels.Users;

namespace Application.System.Users
{
    public interface IUserService
    {

        public Task<BaseResponse<UserModels>> LoginGoogle(LoginGoogleRequest request);
        public Task<BaseResponse<UserModels>> UpdateRole(UpdateRoleRequest request);


        public Task<BaseResponse<UserDTO>> Login(LoginRequestDTO request);

        Task<RegisterResponseDTO> Register(RegisterRequestDTO request);
        public Task<BaseResponse<Token>> GenerateToken(UserModels request);
        Task<BaseResponse<UserDetailDTO>> RefreshToken(RefreshTokenResponse refreshToken);
        Task<BaseResponse<UserDetailDTO>> GetProfile(Guid userID);

        Task<BasePagination<List<UserDetailDTO>>> GetContractorFavorite(PaginationFilter request);
        Task<BasePagination<List<UserDetailDTO>>> GetBuilderFavorite(PaginationFilter request);
        Task<BasePagination<List<UserDetailDTO>>> GetStoreFavorite(PaginationFilter request);
        Task<BasePagination<List<UserDetailDTO>>> GetAllBuilder(PaginationFilter request);
        Task<BaseResponse<string>> ResetPassword(ResetPasswordDTO request);

        Task<BaseResponse<string>> UpdateBuilderProfile(UpdateBuilderRequest request, Guid userID);
        Task<BaseResponse<string>> UpdateContractorProfile(UpdateContractorRequest request, Guid userID);
        Task<BaseResponse<string>> UpdateStoreProfile(UpdateStoreRequest request, Guid userID);
        Task<BaseResponse<UserDetailDTO>> GetProfile(RefreshToken refreshToken);
        Task<BaseResponse<UserCountDTO>> GetTotalUser();
        Task<BasePagination<List<UserDetailDTO>>> GetAllUser(PaginationFilter request);

    }
}
