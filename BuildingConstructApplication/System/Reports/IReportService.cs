using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ContractorPost;
using ViewModels.MaterialStore;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.Reports
{
    public interface IReportService
    {
        Task<BasePagination<List<ReportProductDTO>>> GetAllReportProduct(PaginationFilter filter);

        Task<BaseResponse<string>> ReportProduct(ReportRequestDTO report);
        Task<BasePagination<List<ReportPostDTO>>> GetAllReportPost(PaginationFilter filter);
        Task<BaseResponse<string>> ReportPost(ReportRequestDTO report);


    }
}
