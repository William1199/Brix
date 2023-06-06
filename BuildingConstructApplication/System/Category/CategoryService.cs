using Data.DataContext;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Categories;
using ViewModels.Pagination;
using ViewModels.Response;
using Data.Entities;

namespace Application.System.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly BuildingConstructDbContext _context;

        public CategoryService(BuildingConstructDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCategory(CategoryDTO request)
        {
            var category = new Data.Entities.Categories();
            category.Name= request.CategoryName;
            await _context.Categories.AddAsync(category);
            var rs =await _context.SaveChangesAsync();
            if (rs>0)
            {
                return true;
            }
            return false;
        }

        public async Task<BaseResponse<List<CategoryDTO>>> GetAllCategory(PaginationFilter filter)
        {
            BasePagination<List<CategoryDTO>> response;
            var orderBy = filter._orderBy.ToString();
            int totalRecord;
            orderBy = orderBy switch
            {
                "1" => "ascending",
                "-1" => "descending",
                _ => orderBy
            };

            if (string.IsNullOrEmpty(filter._sortBy))
            {
                filter._sortBy = "Id";
            }

            var result = await _context.Categories.AsNoTracking()
                     .OrderBy("Id" + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .ToListAsync();



            totalRecord = await _context.Categories.CountAsync();

            if (!result.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new(),
                    Pagination = null
                };
            }
            else
            {
                double totalPages;

                totalPages = ((double)totalRecord / (double)filter.PageSize);

                var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                Pagination pagination = new()
                {
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalPages = roundedTotalPages,
                    TotalRecords = totalRecord
                };

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = await MapToCategory(result),
                    Pagination = pagination
                };
            }
            return response;
        }
        private async Task<List<CategoryDTO>> MapToCategory(List<Categories> category)
        {
            List<CategoryDTO> listcate = new();
            foreach (var item in category)
            {
                CategoryDTO cateDTO = new()
                {
                    Id = item.ID,
                    CategoryName = item.Name,
                };
                listcate.Add(cateDTO);
            }
            return listcate;
        }

        public async Task<BaseResponse<CategoryDTO>> GetCateByID(int cateID)
        {
            var response = new BaseResponse<CategoryDTO>();
            var rs = await _context.Categories.Where(x => x.ID == cateID).SingleOrDefaultAsync();
            if (rs != null)
            {
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Data = new();
                response.Data.Id = rs.ID;
                response.Data.CategoryName = rs.Name;
            }
            else
            {
                response.Code = BaseCode.ERROR;
                response.Message = BaseCode.ERROR_MESSAGE;
                response.Data = null;
            }
            return response;
        }
    }
}
