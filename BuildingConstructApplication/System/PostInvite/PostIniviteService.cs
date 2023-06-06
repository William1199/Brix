using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ViewModels.Pagination;
using ViewModels.PostInvite;
using ViewModels.Response;

namespace Application.System.PostInvite
{
    public class PostIniviteService : IPostInviteService
    {
        private readonly BuildingConstructDbContext _context;

        public PostIniviteService(BuildingConstructDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<List<PostInviteDTO>>> CheckInvite(int builderID, Guid userId)
        {
            BaseResponse<List<PostInviteDTO>> response;


            var contractor = await _context.Users
                .Include(x => x.Contractor)
                .FirstOrDefaultAsync(x => x.Id.Equals(userId));

            var builder = await _context.Users
                .Include(x => x.Builder)
                .FirstOrDefaultAsync(x => x.BuilderId.Equals(builderID));


            List<Data.Entities.ContractorPost>? postAlreadyApllied = new();

            if (contractor != null && builder != null)
            {

                var allPost = await _context.ContractorPosts
                    .Where(x => x.ContractorID == contractor.ContractorId && x.Status == Status.SUCCESS)
                    .ToListAsync();

                foreach (var post in allPost)
                {

                    if (_context.PostInvites.Any(x => x.BuilderId == builderID && x.ContractorPostId == post.Id))
                    {
                        postAlreadyApllied.Add(post);
                    }
                    else if (_context.AppliedPosts.Any(x => x.BuilderID == builderID && x.PostID == post.Id))
                    {
                        postAlreadyApllied.Add(post);

                    }
                    else if (_context.PostCommitments.Any(x => x.BuilderID == builderID && x.PostID == post.Id))
                    {
                        postAlreadyApllied.Add(post);
                    }
                }



                var distinctAlreadyList = postAlreadyApllied.Distinct().ToList();
                var result = allPost.Except(distinctAlreadyList).ToList();
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = MapListCheckInivteDTO(result, builder, contractor)
                };

                return response;

            }


            response = new()
            {
                Code = BaseCode.ERROR,
                Message = BaseCode.NOTFOUND_MESSAGE,
            };

            return response;


        }

        public async Task<BaseResponse<string>> Create(CreatePostIniviteRequest requests)
        {
            BaseResponse<string> response;


            Data.Entities.PostInvite postInvite = new()
            {
                BuilderId = requests.BuilderId,
                ContractorId = requests.ContractorId,
                ContractorPostId = requests.ContractorPostId,
                IsRead = false,
                LastModifiedAt = DateTime.Now,
            };

            await _context.PostInvites.AddAsync(postInvite);
            await _context.SaveChangesAsync();


            var sendID = await _context.Users.Where(x => x.BuilderId == requests.BuilderId).Select(x => x.Id).FirstOrDefaultAsync();

            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.SUCCESS_MESSAGE,
                Data = sendID.ToString(),
                NavigateId = requests.ContractorPostId
            };

            return response;


        }

        public async Task<BasePagination<List<PostInviteDTO>>> GetAll(PaginationFilter filter, Guid UserID)
        {
            BasePagination<List<PostInviteDTO>> response;
            var orderBy = filter._orderBy.ToString();
            int totalRecords = 0;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(UserID));

            List<Data.Entities.PostInvite>? data;
            var query = _context.PostInvites;

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
            if (user?.ContractorId != null)
            {
                data = await query
                  .Include(x => x.Contractor)
                        .ThenInclude(x => x.User)
                  .Include(x => x.Builder)
                        .ThenInclude(x => x.User)
                  .Include(x => x.ContractorPost)
                        .OrderBy(filter._sortBy + " " + orderBy)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .Where(x => x.ContractorId == user.ContractorId)
                        .ToListAsync();

                totalRecords = await _context.PostInvites.Where(x => x.ContractorId == user.ContractorId).CountAsync();
            }
            else
            {
                data = await query
                      .Include(x => x.Contractor)
                            .ThenInclude(x => x.User)
                      .Include(x => x.Builder)
                            .ThenInclude(x => x.User)
                      .Include(x => x.ContractorPost)
                      .OrderBy(filter._sortBy + " " + orderBy)
                      .Skip((filter.PageNumber - 1) * filter.PageSize)
                      .Take(filter.PageSize)
                      .Where(x => x.BuilderId == user.BuilderId)
                      .ToListAsync();
                totalRecords = await _context.PostInvites.Where(x => x.BuilderId == user.BuilderId).CountAsync();
            }


            if (!data.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new(),
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

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = MapListDTO(data),
                    Pagination = pagination
                };
            }

            return response;

        }

        public async Task<BaseResponse<bool>> isInvite(int builderID, int postID)
        {
            BaseResponse<bool> response = new();

            var check = await _context.PostInvites.Where(x => x.BuilderId == builderID && x.ContractorPostId == postID).FirstOrDefaultAsync();

            if (check != null)
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = true,
                };
            }
            else
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = false,
                };
            }

            return response;
        }

        public async Task<BaseResponse<string>> Update(int id)
        {
            BaseResponse<string> response;

            var rs = await _context.PostInvites.FirstOrDefaultAsync(x => x.Id == id);
            if (rs != null)
            {
                rs.IsRead = true;
                rs.LastModifiedAt = DateTime.Now;
                _context.Update(rs);
                await _context.SaveChangesAsync();


                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                };
            }
            else
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.NOTFOUND_MESSAGE,
                };

            }

            return response;
        }

        private List<PostInviteDTO> MapListDTO(List<Data.Entities.PostInvite> invites)
        {

            List<PostInviteDTO> ls = new();

            foreach (var item in invites)
            {
                PostInviteDTO tmp = new()
                {
                    BuilderId = item.BuilderId,
                    BuilderName = item.Builder.User.FirstName + " " + item.Builder.User.LastName,
                    CompanyName = item.Contractor.CompanyName,
                    ContractorId = item.ContractorId,
                    IsRead = item.IsRead,
                    LastModifiedAt = item.LastModifiedAt,
                    ContractorName = item.Contractor.User.FirstName + " " + item.Contractor.User.LastName,
                    ContractorPostId = item.ContractorPostId,
                    ContractorPostName = item.ContractorPost.ProjectName,
                    Id = item.Id,
                    Status=item.ContractorPost.Status,
                    Places = item.ContractorPost.Place,
                    Salaries = item.ContractorPost.Salaries,
                };

                var save = _context.Saves.Where(x => x.UserId.Equals(item.ContractorPost.CreateBy) && x.ContractorPostId == item.ContractorPostId).FirstOrDefault();
                if (save != null)
                {
                    tmp.IsSave = true;
                }
                else
                {
                    tmp.IsSave = false;
                }

                ls.Add(tmp);
            }

            return ls;

        }



        private List<PostInviteDTO> MapListCheckInivteDTO(List<Data.Entities.ContractorPost> post, User builder, User contractor)
        {

            List<PostInviteDTO> ls = new();

            foreach (var item in post)
            {
                PostInviteDTO tmp = new()
                {
                    BuilderId = builder.BuilderId,
                    BuilderName = builder.FirstName + " " + builder.LastName,
                    CompanyName = contractor.Contractor.CompanyName,
                    ContractorId = contractor.ContractorId,
                    IsRead = false,
                    LastModifiedAt = item.LastModifiedAt,
                    ContractorName = contractor.FirstName + " " + contractor.LastName,
                    ContractorPostId = item.Id,
                    ContractorPostName = item.ProjectName,
                    Places = item.Place,
                    Salaries = item.Salaries,
                };
                ls.Add(tmp);
            }

            return ls;

        }


    }
}
