using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Carts;
using ViewModels.Commitment;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.Carts
{
    public interface ICartService
    {
        Task<BasePagination<List<CartDTO>>> GetAll(Guid userID,PaginationFilter filter);

        Task<BaseResponse<CartDTO>> Create(Guid userID, CreateCartRequest requests);

        Task<BaseResponse<List<CartDTO>>> Update(Guid userID, List<CreateCartRequest> requests);

        Task<BaseResponse<string>> Remove(Guid userID, List<RemoveCartRequest> requests);

    }
}
