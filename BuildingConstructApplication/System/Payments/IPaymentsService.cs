using ViewModels.Pagination;
using ViewModels.Payment;
using ViewModels.Response;
using ViewModels.Users;

namespace Application.System.Payments
{
    public interface IPaymentsService
    {
        Task<BaseResponse<RefundDTO>> CheckRefundPayment();
        Task<BaseResponse<bool>> UpdateIsRefund();
        Task<BasePagination<List<PaymentDTO>>> PaymentList();
        Task<BasePagination<List<UserPaymentDTO>>> GetTop5PaymentContractor();
        Task<BasePagination<List<UserPaymentDTO>>> GetTop5PaymentStore();
        Task<BasePagination<List<StoreOrderStatistic>>> GetTop5OrderStore();
        Task<BasePagination<List<PaymentDTO>>> PaymentListByUser(Guid userId);
    }
}
