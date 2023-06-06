using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Categories;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.Category
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CategoryDTO request);
        Task<BaseResponse<CategoryDTO>> GetCateByID(int cateID);
        Task<BaseResponse<List<CategoryDTO>>> GetAllCategory(PaginationFilter filter);
    }
}
