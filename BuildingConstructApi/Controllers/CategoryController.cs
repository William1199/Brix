using Application.System.Category;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Categories;
using ViewModels.Pagination;
using ViewModels.Response;

namespace BuildingConstructApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllCategory([FromQuery] PaginationFilter request)
        {
            var validFilter = new PaginationFilter();

            if (request.FilterRequest == null)
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            }
            else
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy, request.FilterRequest);

            }
            var result = await _categoryService.GetAllCategory(request);
            return Ok(result);
        }
        [HttpPost("GetCategoryByID")]
        public async Task<IActionResult> GetCategoryByID([FromQuery] int cateID)
        {
            BaseResponse<CategoryDTO> response = new();
            var rs = await _categoryService.GetCateByID(cateID);
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> createCategory([FromBody] CategoryDTO request)
        {
            BaseResponse<CategoryDTO> response = new();
            var rs = await _categoryService.CreateCategory(request);
            if (rs)
            {
                response.Data = request;
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
            }
            else
            {
                response.Data = null;
                response.Code = BaseCode.ERROR;
                response.Message = BaseCode.ERROR_MESSAGE;
            }
            return Ok(response);
        }
    }
}
