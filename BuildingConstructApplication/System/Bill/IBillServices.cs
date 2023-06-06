using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.BillModels;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.Bill
{
    public interface IBillServices
    {
        Task<BaseResponse<string>> CreateBill(List<BillDTO> requests);
        Task<BasePagination<List<BillDTO>>> GetAllBill(PaginationFilter filter);

        Task<BaseResponse<BillDetailDTO>> GetDetail(int billID);

        Task<BaseResponse<SmallBillDetailDTO>> GetDetailBySmallBill(int billID);

        Task<BaseResponse<List<Cart>>> UpdateStatusBill(Status status, int billID, string message,Guid userID);

        Task<BasePagination<List<ProductBillDetail>>> GetHistoryProductBill(Guid userID, PaginationFilter request);
    }
}
