using Application.System.MaterialStores;
using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;
using ViewModels.Categories;
using ViewModels.ContractorPost;
using ViewModels.MaterialStore;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.Reports
{
    public class ReportService : IReportService
    {
        private readonly BuildingConstructDbContext _context;
        private IHttpContextAccessor _accessor;
        public ReportService(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<BasePagination<List<ReportPostDTO>>> GetAllReportPost(PaginationFilter filter)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var userID = identifierClaim.Value.ToString();
            BasePagination<List<ReportPostDTO>> response;
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



            IQueryable<ContractorPost> query = _context.ContractorPosts.Where(x=>x.CreateBy.ToString().Equals(userID)).Include(x => x.Reports);
            StringBuilder salariesSearch = new();
            StringBuilder placeSearch = new();
            StringBuilder categoriesSearch = new();
            StringBuilder typesSearch = new();

            if (filter.FilterRequest != null)
            {
                if (filter.FilterRequest.Salary.Any())
                {
                    var count = filter.FilterRequest.Salary.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (i == count - 1)
                        {
                            salariesSearch.Append("Salaries=*" + filter.FilterRequest.Salary[i]);
                            break;
                        }
                        salariesSearch.Append("Salaries=*" + filter.FilterRequest.Salary[i] + "|");
                    }
                    query = query.ApplyFiltering(salariesSearch.ToString());
                }

                if (filter.FilterRequest.Places.Any())
                {
                    var count = filter.FilterRequest.Places.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (i == count - 1)
                        {
                            placeSearch.Append("Place=" + filter.FilterRequest.Places[i]);
                            break;
                        }
                        placeSearch.Append("Place=" + filter.FilterRequest.Places[i] + "|");
                    }
                    query = query.ApplyFiltering(placeSearch.ToString());
                }

                if (filter.FilterRequest.Categories.Any())
                {
                    var count = filter.FilterRequest.Categories.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (i == count - 1)
                        {
                            categoriesSearch.Append("PostCategories=" + filter.FilterRequest.Categories[i]);
                            break;
                        }
                        categoriesSearch.Append("PostCategories=" + filter.FilterRequest.Categories[i] + "|");
                    }
                    query = query.ApplyFiltering(categoriesSearch.ToString());
                }

                if (filter.FilterRequest.Participant.HasValue)
                {
                    query = query.Where(x => x.NumberPeople == filter.FilterRequest.Participant);
                }

                if (!string.IsNullOrEmpty(filter.FilterRequest.Title))
                {
                    query = query.Include(x => x.Contractor).Where(x => x.Title.Contains(filter.FilterRequest.Title) || x.Contractor.CompanyName.Contains(filter.FilterRequest.Title));
                }
                if (filter.FilterRequest.Transport == true)
                {
                    query = query.Where(x => x.Transport == true);

                }
                if (filter.FilterRequest.Accommodation == true)
                {
                    query = query.Where(x => x.Accommodation == true);

                }

                if (filter.FilterRequest.Types.Any())
                {
                    var count = filter.FilterRequest.Types.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (i == count - 1)
                        {
                            typesSearch.Append("TypeID=" + filter.FilterRequest.Types[i]);
                            break;
                        }
                        typesSearch.Append("TypeID=" + filter.FilterRequest.Types[i] + "|");
                    }

                    var types = await _context.ContractorPostTypes.ApplyFiltering(typesSearch.ToString()).Select(x => x.ContractorPostID).ToListAsync();


                    query = query.Where(x => types.Contains(x.Id));

                }

            }

            var result = await query
                    .Include(x => x.Contractor)
                        .ThenInclude(x => x.User)
                    .Where(x =>x.Reports.Any())
                     .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .ToListAsync();


            if (filter.FilterRequest != null)
            {
                totalRecord = result.Where(x => x.Reports.Any()).Count(); ;
            }
            else
            {
                totalRecord = await _context.ContractorPosts.CountAsync();
            }

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
                    Data =await MapListPostDTO(result, Guid.Parse(userID)),
                    Pagination = pagination
                };
            }
            return response;
        }
        private async Task<List<ReportPostDTO>> MapListPostDTO(List<ContractorPost> list, Guid id)
        {
            List<ReportPostDTO> result = new();

            foreach (var item in list)
            {
                //var user = _context.Users.Where(x => x.ContractorId.Equals(item.ContractorID)).FirstOrDefault();
                var isSaved = _context.Saves.Where(x => x.ContractorPostId == item.Id && x.UserId.Equals(id)).FirstOrDefault();
                bool save = false;
                if (isSaved != null)
                {
                    save = true;
                }


                ReportPostDTO dto = new()
                {
                    Avatar = item.Contractor.User.Avatar,
                    ContractorID = item.ContractorID,
                    Description = item.Description,
                    EndDate = item.EndDate,
                    Id = item.Id,
                    NumberPeople = item.NumberPeople,
                    Place = item.Place,
                    PostCategories = item.PostCategories,
                    ProjectName = item.ProjectName,
                    Salaries = item.Salaries,
                    StarDate = item.StarDate,
                    AuthorName = item.Contractor.User.FirstName + " " + item.Contractor.User.LastName,
                    Title = item.Title,
                    IsSave = save,
                    _createdBy = item.CreateBy,
                    ReportCount = item.Reports.Count(),
                    ViewCount = item.ViewCount,
                    LastModifiedAt = item.LastModifiedAt,
                    Problems=await MapReportProblem(item.Id),
                    Status = item.Status
                };
                result.Add(dto);
            }
            return result;
        }
        private async Task<List<Problems>> MapReportProblem(int Id)
        {
            List<Problems> result = new();
            var query = await _context.Reports.Where(x => x.ProductId == Id).ToListAsync();

            if (query.Count== 0)
            {
                query= await _context.Reports.Where(x => x.ContractorPostId == Id).ToListAsync();
            }

            foreach(var item in query)
            {
                var problem = new Problems();
                problem.Problem = item.ReportProblem;
                result.Add(problem);
            }
            return result;
        }

        public async Task<BasePagination<List<ReportProductDTO>>> GetAllReportProduct(PaginationFilter filter)
        {
            BasePagination<List<ReportProductDTO>> response;
            var orderBy = filter._orderBy.ToString();
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var userID = identifierClaim.Value.ToString();
            var storeID = await _context.Users.Where(x => x.Id.ToString().Equals(userID)).Select(x=>x.MaterialStoreID).FirstOrDefaultAsync();
            if (string.IsNullOrEmpty(filter._sortBy))
            {
                filter._sortBy = "Id";
            }
            orderBy = orderBy switch
            {
                "1" => "ascending",
                "-1" => "descending",
                _ => orderBy
            };
            IQueryable<Products> query = (IQueryable<Products>)_context.Products.Include(x => x.ProductCategories).Include(x => x.Reports).Include(x => x.MaterialStore).ThenInclude(x => x.User);
            //select new
            //{
            //    Product=p,
            //    ReportCount=p.Reports.Count()
            //});


            var data = await query
                .AsNoTracking()
                .Where(x => x.Reports.Any() && x.MaterialStoreID == storeID)
               .OrderBy(filter._sortBy + " " + orderBy)
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();


            var totalRecords = await _context.Products.Include
                (x => x.Reports).Where(x => x.Reports.Any()).CountAsync();

            if (!data.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new List<ReportProductDTO>(),
                };
            }
            else
            {
                double totalPages;

                totalPages = totalRecords / (double)filter.PageSize;

                var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                Pagination pagination = new()
                {
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalPages = roundedTotalPages,
                    TotalRecords = totalRecords
                };

                response = new();
                response.Data = new();
                foreach (var item in data)
                {
                    ReportProductDTO dto = new();
                    dto.Id = item.Id;
                    dto.Name = item.Name;
                    dto.Description = item.Description;
                    dto.Brand = item.Brand;
                    dto.UnitInStock = item.UnitInStock;
                    dto.UnitPrice = item.UnitPrice;
                    dto.SoldQuantities = item.SoldQuantities;
                    dto.LastModifiedAt = item.LastModifiedAt;
                    dto.Problem = await MapReportProblem(item.Id);
                    dto.ReportCount = item.Reports.Count();
                    string img = item?.Image;
                    if (img != null)
                    {
                        string[] firstImg = img.Split(",");
                        dto.Image = firstImg[0].Trim();
                    }
                    else
                    {
                        dto.Image = "";
                    }
                    dto.StoreName = item.MaterialStore?.User?.FirstName + item.MaterialStore?.User?.LastName;
                    dto.StoreID = item.MaterialStoreID;
                    dto.StoreImage = item.MaterialStore?.Image;
                    dto.Status = item.Status;
                    dto.ProductCategories = await GetCategory(item.ProductCategories);
                    response.Data.Add(dto);
                }
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Pagination = pagination;
            }
            return response;
        }

        public async Task<List<CategoryDTO>> GetCategory(List<ProductCategories> productCategories)
        {
            List<CategoryDTO> list = new();
            foreach (var c in productCategories)
            {
                var final = new CategoryDTO();
                final.Id = c.CategoriesID;
                final.CategoryName = c.Name;
                final.Name = c.Name;
                list.Add(final);
            }
            return list;
        }

        public async Task<BaseResponse<string>> ReportPost(ReportRequestDTO report)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var response = new BaseResponse<string>();
            var userID = identifierClaim.Value.ToString();
            var checkrp = await _context.Reports.Where(x => x.ContractorPostId == report.ContractorPostId && x.CreateBy == Guid.Parse(userID)).CountAsync();

            var author = await _context.ContractorPosts.Where(x => x.Id == report.ContractorPostId).FirstOrDefaultAsync();
            if (checkrp > 0)
            {
                response.Code = BaseCode.ERROR;
                response.Data = author.CreateBy.ToString();
                response.Message = "Bạn đã report bài post này rồi";
                return response;
            }
            var rp = new Report()
            {
                ContractorPostId = report.ContractorPostId,
                LastModifiedAt = DateTime.Now,
                CreateBy = Guid.Parse(userID),
                UserId=Guid.Parse(userID),
                ReportProblem = report.ReportProblem
            };
            await _context.Reports.AddAsync(rp);
            var rs = await _context.SaveChangesAsync();
            var count = await _context.Reports.Where(x => x.ContractorPostId == report.ContractorPostId).CountAsync();
            if (count == 5)
            {
                var pd = await _context.ContractorPosts.Where(x => x.Id == report.ContractorPostId).FirstOrDefaultAsync();
                pd.Status = Status.DELEDTED;
                _context.ContractorPosts.Update(pd);
                await _context.SaveChangesAsync();
            }

            if (rs > 0)
            {
                response.Code = BaseCode.SUCCESS;
                response.Message = "Report thành công";
                response.Data = author.CreateBy.ToString();
                response.NavigateId = report.ContractorPostId;
            }
            else
            {
                response.Code = BaseCode.ERROR;
                response.Data = null;
                response.Message = "Report không thành công";
            }
            return response;
        }

        public async Task<BaseResponse<string>> ReportProduct(ReportRequestDTO report)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var response = new BaseResponse<string>();
            var userID = identifierClaim.Value.ToString();
            var checkrp = await _context.Reports.Where(x => x.ProductId == report.ProductId && x.CreateBy == Guid.Parse(userID)).CountAsync();
            var author = await _context.Products.Include(x=>x.MaterialStore).ThenInclude(x=>x.User).Where(x => x.Id == report.ProductId).Select(x=>x.MaterialStore.User.Id).FirstOrDefaultAsync();

            if (checkrp > 0)
            {
                response.Code = BaseCode.ERROR;
                response.Data = author.ToString();
                response.Message = "Bạn đã report sản phẩm này rồi";
                return response;
            }
            var rp = new Report()
            {
                ProductId = report.ProductId,
                LastModifiedAt = DateTime.Now,
                CreateBy = Guid.Parse(userID),
                UserId = Guid.Parse(userID),
                ReportProblem = report.ReportProblem
            };
            await _context.Reports.AddAsync(rp);
            var rs = await _context.SaveChangesAsync();
            var count = await _context.Reports.Where(x => x.ProductId == report.ProductId).CountAsync();
            if (count == 5)
            {
                var pd = await _context.Products.Where(x => x.Id == report.ProductId).FirstOrDefaultAsync();
                pd.Status = false;
                _context.Products.Update(pd);
                await _context.SaveChangesAsync();
            }

            if (rs > 0)
            {
                response.Code = BaseCode.SUCCESS;
                response.Message = "Report thành công";
                response.Data = author.ToString();
                response.NavigateId = report.ProductId;
            }
            else
            {
                response.Code = BaseCode.ERROR;
                response.Data = null;
                response.Message = "Report không thành công";
            }
            return response;
        }


    }
}
