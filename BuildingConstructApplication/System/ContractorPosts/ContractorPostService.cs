using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;
using ViewModels.Commitment;
using ViewModels.ContractorPost;
using ViewModels.MaterialStore;
using ViewModels.Pagination;
using ViewModels.Quizzes;
using ViewModels.Response;
using ViewModels.Users;


namespace Application.System.ContractorPosts
{
    public class ContractorPostService : IContractorPostService
    {
        private readonly BuildingConstructDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public ContractorPostService(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<bool> CreateContractorPost(ContractorPostModels contractorPostDTO)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var userID = identifierClaim?.Value;
            var contracID = _context.Users.Where(x => x.Id.ToString().Equals(userID)).FirstOrDefault().ContractorId;

            var contractorPost = new ContractorPost
            {
                Title = contractorPostDTO.Title,
                ProjectName = contractorPostDTO.ProjectName,
                Description = contractorPostDTO.Description,
                StarDate = contractorPostDTO.StarDate,
                EndDate = contractorPostDTO.EndDate,
                ConstructionType = contractorPostDTO.ConstructionType,
                StartTime = contractorPostDTO.StartTime,
                EndTime = contractorPostDTO.EndTime,
                Accommodation = contractorPostDTO.Accommodation,
                Transport = contractorPostDTO.Transport,
                Place = contractorPostDTO.Place,
                Salaries = contractorPostDTO.Salaries,
                ViewCount = 0,
                NumberPeople = contractorPostDTO.NumberPeople,
                PeopeRemained = contractorPostDTO.NumberPeople,
                Status = contractorPostDTO.QuizRequired == true ? Status.PENDING : Status.SUCCESS,
                PostCategories = contractorPostDTO.PostCategories,
                Benefit = contractorPostDTO.Benefit,
                Required = contractorPostDTO.Required,
                LastModifiedAt = DateTime.Now,
                CreateBy = Guid.Parse(userID),
                ContractorID = (int)contracID,
                QuizRequired = contractorPostDTO.QuizRequired,
                VideoRequired = contractorPostDTO.VideoRequired,
            };

            await _context.ContractorPosts.AddAsync(contractorPost);
            await _context.SaveChangesAsync();
            //first
            var id = contractorPost.Id;
            var type = contractorPostDTO.type;
            foreach (var item in type)
            {
                var rType = new Data.Entities.ContractorPostType();
                if (item.id != null)
                {
                    rType.TypeID = (Guid)item.id;
                    rType.ContractorPostID = id;
                    _context.ContractorPostTypes.Add(rType);
                    var rs = _context.SaveChanges();
                    if (rs < 0)
                    {
                        _context.Remove(contractorPost);
                        return false;

                    }
                }
            }

            var flag = false;
            foreach (var i in type)
            {
                foreach (var o in i.SkillArr)
                {

                    if (o.fromSystem == false)
                    {
                        var rSkill = new Skill
                        {
                            Name = o.name,
                            FromSystem = o.fromSystem
                        };
                        _context.Skills.Add(rSkill);
                        _context.SaveChanges();



                        var cPostSkill = new ContractorPostSkill
                        {
                            ContractorPostID = id,
                            SkillID = rSkill.Id
                        };
                        _context.ContractorPostSkills.Add(cPostSkill);
                        var rs = _context.SaveChanges();
                        if (rs < 0)
                        {
                            _context.Remove(contractorPost);
                            return false;
                        }
                        flag = true;
                    }
                    else
                    {
                        var rs = await _context.Skills.Include(x => x.Type).Where(x => x.Id == o.id).ToListAsync();
                        foreach (var c in rs)
                        {
                            var guid = i.id.ToString();
                            var cid = c.TypeId.ToString();
                            if (guid.Equals(cid))
                            {

                                var cPostSkill = new ContractorPostSkill
                                {
                                    ContractorPostID = id,
                                    SkillID = o.id
                                };
                                _context.ContractorPostSkills.Add(cPostSkill);
                                _context.SaveChanges();
                                flag = true;
                            }
                            else
                            {
                                _context.ContractorPosts.Remove(contractorPost);
                                _context.SaveChanges();
                                return false;
                            }
                        }
                    }
                }

            };
            await _context.SaveChangesAsync();
            if (flag)
            {
                return true;
            }
            return false;
        }

        public async Task<BaseResponse<ContractorPostDetailDTO>> GetDetailPost(int cPostid, int? pageSize)
        {
            var rs = await _context.ContractorPosts.FirstOrDefaultAsync(x => x.Id == cPostid);
            BaseResponse<ContractorPostDetailDTO> response = new();
            var userId = _accessor.HttpContext?.User.FindFirst("UserID")?.Value.ToString();

            if (rs == null)
            {

                response.Code = BaseCode.ERROR;
                response.Message = "Cannot find that Post";
                return response;
            }
            var count = await _context.ContractorPosts.Where(x => x.Id == cPostid).ToListAsync();
            if (userId != null)
            {
                if (!rs.CreateBy.ToString().Equals(userId))
                {
                    foreach (var item in count)
                    {
                        int view = item.ViewCount;
                        view++;
                        item.ViewCount = view;
                        _context.ContractorPosts.Update(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            ContractorPostDetailDTO postDetail = await MapToDetailDTO(rs, userId, pageSize);
            response.Data = postDetail;
            response.Code = BaseCode.SUCCESS;
            response.Message = "SUCCESS";
            return response;
        }
        private async Task<ContractorPostDetailDTO> MapToDetailDTO(ContractorPost post, string userID, int? pageSize)
        {
            bool IsSave = false;

            var save = await _context.Saves.Where(x => x.UserId.ToString().Equals(userID) && x.ContractorPostId == post.Id).FirstOrDefaultAsync();
            if (save != null)
            {
                IsSave = true;
            }
            var builder = await _context.Builders.Where(x => x.User.Id.ToString().Equals(userID)).FirstOrDefaultAsync();
            dynamic c = null;
            if (builder != null)
            {
                var check = await _context.AppliedPosts.Where(x => x.BuilderID == builder.Id && x.ContractorPosts.Id == post.Id).FirstOrDefaultAsync();
                c = check;
                if (check != null)
                {
                    post.isApplied = true;

                }
                else
                {
                    var alreadyCommitment = await _context.PostCommitments.Where(x => x.BuilderID == builder.Id && x.ContractorPosts.Id == post.Id).FirstOrDefaultAsync();
                    if (alreadyCommitment != null)
                    {
                        post.isApplied = true;
                    }
                    else
                    {
                        post.isApplied = false;

                    }
                }

            }
            else
            {
                post.isApplied = false;
            }

            var listQuiz = await _context.Quizzes.Where(x => x.PostID == post.Id).ToListAsync();


            ContractorPostDetailDTO postDTO = new()
            {
                Title = post.Title,
                Accommodation = post.Accommodation,
                ConstructionType = post.ConstructionType,
                EndTime = post.EndTime,
                StartTime = post.StartTime,
                Transport = post.Transport,
                ProjectName = post.ProjectName,
                Salaries = post.Salaries,
                Description = post.Description,
                Benefit = post.Benefit,
                Required = post.Required,
                StarDate = post.StarDate,
                ViewCount = post.ViewCount,
                EndDate = post.EndDate,
                LastModifiedAt = post.LastModifiedAt,
                NumberPeople = post.NumberPeople,
                PeopleRemained = post.PeopeRemained,
                PostCategories = post.PostCategories,
                Place = post.Place,
                IsGroup = c?.GroupID == null ? false : true,
                QuizId = c?.QuizId != null ? c.QuizId : null,
                IsApplied = post.isApplied,
                RequiredQuiz = post.QuizRequired,
                VideoRequired = post.VideoRequired,
                type = await GetTypeAndSkillFromPost(post.Id),
                CreatedBy = post.CreateBy,
                Author = await GetUserProfile(post.CreateBy),
                IsSave = IsSave,
                Status = post.Status,
                Quizzes = listQuiz.Any() ? listQuiz : null,
            };

            //checking hasAppliedQuiz 
            if (builder != null)
            {
            var appliedQuiz =await _context.UserAnswers.Where(x => x.BuilderId == builder.Id && listQuiz.Contains(x.Answer.Question.Quiz)).ToListAsync();
            if (appliedQuiz.Any())
            {
                postDTO.IsQuizAnswer = true;
            }

            }





            //checking recommended
            string keyword = string.Empty;
            switch (post.PostCategories)
            {
                case PostCategories.CO_DIEN:
                    keyword = "Cổ điển ";
                    break;
                case PostCategories.HIEN_DAI:
                    keyword = "Hiện đại";
                    break;
                case PostCategories.TAN_CO_DIEN:
                    keyword = "Tân cổ điển";
                    break;
            }

            var listStore = _context.ProductCategories
                            .Where(x => x.CategoriesID == 3 && x.Name.Equals(keyword))
                            .Select(x => x.Products.MaterialStoreID)
                            .Distinct().ToList();

            List<int> result = new();

            foreach (var item in listStore)
            {
                result.Add(item.Value);
            }


            if (listStore.Count < pageSize)
            {
                var remaining = pageSize - listStore.Count;

                var allStoreId = _context.MaterialStores.Where(x => !listStore.Any(storeID => storeID == x.Id)).Select(x => x.Id).Take(remaining.Value).ToList();

                result.AddRange(allStoreId);

            }

            postDTO.RecommendStore = MapListStoreRecommendedDto(result);


            return postDTO;
        }

        private List<MaterialStoreDTO> MapListStoreRecommendedDto(List<int> listID)
        {
            List<MaterialStoreDTO> result = new();

            foreach (var item in listID)
            {

                var store = _context.MaterialStores.Include(x => x.User).FirstOrDefault(x => x.Id == item);

                MaterialStoreDTO dto = new()
                {

                    Avatar = store.User.Avatar,
                    FirstName = store.User.FirstName,
                    LastName = store.User.LastName,
                    Description = store.Description,
                    Id = store.Id,
                    Place = store.Place,
                    Experience = store.Experience,
                    Image = store.Image,
                    TaxCode = store.TaxCode,
                    Webstie = store.Website,
                    UserId = store.User.Id,

                };
                result.Add(dto);
            }
            return result;
        }













        private async Task<List<TypeModels>> GetTypeAndSkillFromPost(int postID)
        {
            var results = await _context.ContractorPostTypes.Include(x => x.Type).Where(x => x.ContractorPostID == postID).ToListAsync();
            var rsSkill = await _context.ContractorPostSkills.Include(x => x.Skills).Where(x => x.ContractorPostID == postID).ToListAsync();
            var final = new List<TypeModels>();
            foreach (var item in results)
            {
                var type = new TypeModels
                {
                    id = item.TypeID,
                    name = item.Type.Name,
                    SkillArr = new()
                };
                foreach (var i in rsSkill)
                {
                    if (i.Skills.TypeId.Equals(item.TypeID))
                    {
                        var skillArr = new SkillArr
                        {
                            id = i.SkillID,
                            name = i.Skills.Name,
                            fromSystem = i.Skills.FromSystem,
                            TypeId = i.Skills.TypeId
                        };
                        type.SkillArr.Add(skillArr);
                    }
                }
                final.Add(type);
            }
            if (rsSkill.Where(x => x.Skills.TypeId == null).ToList().Any())
            {

                foreach (var i in rsSkill.Where(x => x.Skills.TypeId == null).ToList())
                {
                    var t = new TypeModels();
                    t.SkillArr = new();
                    var skillArr = new SkillArr
                    {
                        id = i.SkillID,
                        name = i.Skills.Name,
                        fromSystem = i.Skills.FromSystem,
                        TypeId = i.Skills.TypeId
                    };
                    t.SkillArr.Add(skillArr);
                    final.Add(t);
                }
            }
            return final;
        }
        public async Task<UserModelsDTO> GetUserProfile(Guid userID)
        {
            var results = await _context.Users.Where(x => x.Id.ToString().Equals(userID.ToString())).SingleOrDefaultAsync();
            var roleid = await _context.UserRoles.Where(x => x.UserId.Equals(userID)).Select(x => x.RoleId).SingleOrDefaultAsync();
            var final = new UserModelsDTO
            {
                UserName = results.UserName,
                Phone = results.PhoneNumber,
                FirstName = results.FirstName,
                LastName = results.LastName,
                Address = results.Address,
                Gender = results.Gender,
                Avatar = results.Avatar,
                DOB = results.DOB,
                Email = results.Email,
                RoleName = await _context.Roles.Where(x => x.Id.Equals(roleid)).Select(x => x.Name).SingleOrDefaultAsync()
            };
            return final;
        }

        public async Task<BasePagination<List<ContractorPostDTO>>> GetPost(PaginationFilter filter, Guid id)
        {
            BasePagination<List<ContractorPostDTO>> response;
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
                filter._sortBy = "LastModifiedAt";
            }



            IQueryable<ContractorPost> query = _context.ContractorPosts;
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
                    .Where(x => x.Status == Status.SUCCESS)
                     .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .ToListAsync();


            if (filter.FilterRequest != null)
            {
                totalRecord = result.Count;
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
                    Data = MapListDTO(result, id),
                    Pagination = pagination
                };
            }
            return response;
        }

        public async Task<BasePagination<List<AppliedPostDTO>>> ViewAppliedPost(int postID, PaginationFilter filter)
        {
            BasePagination<List<AppliedPostDTO>> response;
            var orderBy = filter._orderBy.ToString();

            orderBy = orderBy switch
            {
                "1" => "ascending",
                "-1" => "descending",
                _ => orderBy
            };

            if (string.IsNullOrEmpty(filter._sortBy))
            {
                filter._sortBy = "PostID";
            }


            var appliedPost = await _context.AppliedPosts
                .Include(x => x.Builder)
                    .ThenInclude(x => x.User)
                .Include(x => x.Builder)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Quiz)
                .Where(x => x.PostID == postID)
                .OrderBy(filter._sortBy + " " + orderBy)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking()
                .ToListAsync();


            if (!appliedPost.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new(),
                };
                return response;
            }

            var totalRecord = await _context.AppliedPosts.Where(x => x.PostID == postID).CountAsync();

            List<AppliedPostDTO> appliedPostDTOs = new();
            foreach (var x in appliedPost)
            {
                var flag = await _context.Groups.Where(group => group.BuilderID == x.BuilderID && group.PostID == postID).FirstOrDefaultAsync();
                if (flag == null)
                {
                    appliedPostDTOs.Add(MapToAppliedPostDTO(x));
                }
                else
                {
                    var groups = await _context.GroupMembers.Where(x => x.GroupId == flag.Id).ToListAsync();
                    appliedPostDTOs.Add(MapToAppliedPostGroupDTO(x, groups));
                }

            }
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
                Data = appliedPostDTOs,
                Pagination = pagination
            };

            return response;
        }


        private AppliedPostDTO MapToAppliedPostDTO(AppliedPost applied)
        {
            int totalScore = 0;
            int totalQuestion = 0;

            if (applied.QuizId != null)
            {
                totalScore = _context.UserAnswers
                    .Include(x => x.Answer)
                    .Where(x => x.BuilderId == applied.BuilderID && x.Answer.isCorrect == true && x.Answer.Question.QuizId == applied.QuizId)
                    .Count();

                totalQuestion = _context.Questions.Where(x => x.QuizId == applied.QuizId).Count();

            }



            AppliedPostDTO rs = new()
            {
                Avatar = applied.Builder.User.Avatar,
                BuilderID = applied.BuilderID,
                FirstName = applied.Builder.User.FirstName,
                LastName = applied.Builder.User.LastName,
                UserID = applied.Builder.User.Id,
                WishSalary = applied.WishSalary,
                AppliedDate = applied.AppliedDate,
                QuizId = applied.QuizId,
                QuizName = applied.Quiz?.Name,
                Video = applied.Video,
                TypeName = applied.Builder.Type.Name,
                TotalCorrectAnswers = totalScore,
                TotalNumberQuestion = totalQuestion,


            };
            return rs;
        }

        private AppliedPostDTO MapToAppliedPostGroupDTO(AppliedPost applied, List<GroupMember> groupMember)
        {
            List<AppliedGroup> group = MapGroup(groupMember);
            int totalScore = 0;
            int totalQuestion = 0;

            if (applied.QuizId != null)
            {
                totalScore = _context.UserAnswers
                    .Include(x => x.Answer)
                    .Where(x => x.BuilderId == applied.BuilderID && x.Answer.isCorrect == true && x.Answer.Question.QuizId == applied.QuizId)
                    .Count();
                totalQuestion = _context.Questions.Where(x => x.QuizId == applied.QuizId).Count();

            }
            AppliedPostDTO rs = new()
            {
                Avatar = applied.Builder.User.Avatar,
                BuilderID = applied.BuilderID,
                FirstName = applied.Builder.User.FirstName,
                LastName = applied.Builder.User.LastName,
                UserID = applied.Builder.User.Id,
                WishSalary = applied.WishSalary,
                Video = applied.Video,
                QuizId = applied.QuizId,
                QuizName = applied.Quiz?.Name,
                Groups = group,
                AppliedDate = applied.AppliedDate,
                TotalCorrectAnswers = totalScore,
                TotalNumberQuestion = totalQuestion,

            };
            return rs;
        }
        private List<AppliedGroup> MapGroup(List<GroupMember> groupMembers)
        {
            List<AppliedGroup> result = new();

            foreach (var item in groupMembers)
            {
                var type = _context.Types.Where(x => x.Id == item.TypeID).Select(x => x.Name).FirstOrDefault();

                AppliedGroup rs = new()
                {
                    DOB = item.DOB,
                    VerifyId = item.IdNumber,
                    Name = item.Name,
                    TypeName = type,
                    TypeID = item.TypeID,
                    SkillAssessment = item.SkillAssessment,
                    BehaviourAssessment = item.BehaviourAssessment

                };
                result.Add(rs);
            }
            return result;

        }
        public async Task<BasePagination<List<ContractorPostDTO>>> GetPostByViews(PaginationFilter filter)
        {

            BasePagination<List<ContractorPostDTO>> response;
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
                filter._sortBy = "LastModifiedAt";
            }


            var result = await _context.ContractorPosts
                           .OrderBy("Views" + " " + orderBy)
                           .Skip((filter.PageNumber - 1) * filter.PageSize)
                           .Take(filter.PageSize)
                           .ToListAsync();

            totalRecord = await _context.ContractorPosts.CountAsync();


            if (!result.Any())
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
                    Data = MapListDTO(result),
                    Pagination = pagination
                };
            }
            return response;
        }

        public async Task<BasePagination<List<ContractorPostDTO>>> SearchPost(PaginationFilter filter, string keyword)
        {
            BasePagination<List<ContractorPostDTO>> response;

            var result = await _context.ContractorPosts.Include(x => x.Contractor).Where(x => x.Title.Contains(keyword) || x.Contractor.CompanyName.Contains(keyword)).ToListAsync();


            var totalRecord = result.Count;

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
                    Data = MapListDTO(result),
                    Pagination = pagination
                };
            }
            return response;
        }

        private List<ContractorPostDTO> MapListDTO(List<ContractorPost> list, Guid id)
        {
            List<ContractorPostDTO> result = new();

            foreach (var item in list)
            {
                //var user = _context.Users.Where(x => x.ContractorId.Equals(item.ContractorID)).FirstOrDefault();
                var isSaved = _context.Saves.Where(x => x.ContractorPostId == item.Id && x.UserId.Equals(id)).FirstOrDefault();
                bool save = false;
                if (isSaved != null)
                {
                    save = true;
                }
                ContractorPostDTO dto = new()
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
                    ViewCount = item.ViewCount,
                    LastModifiedAt = item.LastModifiedAt,
                    Status = item.Status
                };
                result.Add(dto);
            }
            return result;
        }

        private List<ContractorPostDTO> MapListDTO(List<ContractorPost> list)
        {
            List<ContractorPostDTO> result = new();

            foreach (var item in list)
            {
                //var user = _context.Users.Where(x => x.ContractorId.Equals(item.ContractorID)).FirstOrDefault();



                ContractorPostDTO dto = new()
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
                    ViewCount = item.ViewCount,
                    LastModifiedAt = item.LastModifiedAt,
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<BaseResponse<string>> AppliedPost(AppliedPostRequest request, Guid userID)
        {
            BaseResponse<string> response;

            var user = await _context.Users.Where(x => x.Id.Equals(userID)).FirstOrDefaultAsync();

            if (user == null)
            {
                response = new()
                {
                    Code = BaseCode.ERROR,
                    Message = BaseCode.NOTFOUND_MESSAGE
                };
                return response;
            }
            else
            {

                var alreadyApplied = await _context.AppliedPosts.Where(x => x.BuilderID == user.BuilderId && x.PostID == request.PostId).FirstOrDefaultAsync();

                if (alreadyApplied != null)
                {
                    response = new()
                    {
                        Code = BaseCode.ERROR,
                        Message = "Bạn đã ứng tuyển vào vị trí này ",
                        Data = "ALREADY_APPLIED"
                    };
                    return response;
                }
                var post = await _context.ContractorPosts.FirstOrDefaultAsync(x => x.Id == request.PostId);

                var commitmentInPost = await _context.PostCommitments.Where(x => x.BuilderID == user.BuilderId && x.PostID == request.PostId).FirstOrDefaultAsync();
                if (commitmentInPost != null)
                {
                    response = new()
                    {
                        Code = BaseCode.ERROR,
                        Message = "Bạn đã ứng tuyển vào vị trí này ",
                        Data = "ALREADY_APPLIED"
                    };
                    return response;
                }


                //first order commitment
                var checkCommitment = await _context.PostCommitments.Where(x => x.BuilderID == user.BuilderId).OrderByDescending(x => x.Id).ToListAsync();


                if (post != null)
                {
                    if (checkCommitment.Any())
                    {
                        if (checkCommitment.First().EndDate > post.StarDate)
                        {
                            response = new()
                            {
                                Code = BaseCode.ERROR,
                                Message = "Bạn đang có một cam kết có hiệu lực",
                                Data = "ALREADY_COMMITMENT"
                            };
                            return response;
                        }
                    }

                }

                if (request.IsGroup == true && request.QuizSubmit != null)
                {
                    List<UserAnswer> ls = new();

                    foreach (var item in request.QuizSubmit.AnswerId)
                    {
                        UserAnswer answer = new()
                        {
                            AnswerID = item,
                            BuilderId = user.BuilderId.Value
                        };

                        ls.Add(answer);

                    }
                    await _context.AddRangeAsync(ls);
                    await _context.SaveChangesAsync();

                    response = new()
                    {
                        Code = BaseCode.SUCCESS,
                        Message = "ALREADY_APPLIED",
                        Data = "ALREADY_APPLIED"
                    };

                    return response;
                }


                //Group is not null
                if (request.GroupMember is not null)
                {
                    Group group = new()
                    {
                        BuilderID = user.BuilderId.Value,
                        PostID = request.PostId
                    };

                    await _context.Groups.AddAsync(group);
                    await _context.SaveChangesAsync();

                    foreach (var item in request.GroupMember)
                    {
                        GroupMember groupMember = new()
                        {
                            DOB = item.DOB,
                            IdNumber = item.VerifyId,
                            Name = item.Name,
                            TypeID = item.TypeID,
                            GroupId = group.Id,
                            BehaviourAssessment = item.BehaviourAssessment,
                            SkillAssessment = item.SkillAssessment
                        };

                        await _context.GroupMembers.AddAsync(groupMember);
                        await _context.SaveChangesAsync();
                    }

                    if (request.QuizSubmit != null)
                    {
                        List<UserAnswer> ls = new();

                        foreach (var item in request.QuizSubmit.AnswerId)
                        {
                            UserAnswer answer = new()
                            {
                                AnswerID = item,
                                BuilderId = user.BuilderId.Value
                            };

                            ls.Add(answer);

                        }
                        await _context.AddRangeAsync(ls);
                        await _context.SaveChangesAsync();

                    }



                    AppliedPost applied = new()
                    {
                        BuilderID = user.BuilderId.Value,
                        PostID = request.PostId,
                        WishSalary = request.WishSalary,
                        GroupID = group.Id,
                        Status = Status.NOT_RESPONSE,
                        AppliedDate = DateTime.Now,
                        Video = request.Video
                    };

                    if (request.QuizSubmit != null)
                    {
                        var quizID = await _context.Answers
                                    .Include(x => x.Question)
                                    .Where(x => x.Id == request.QuizSubmit.AnswerId.First())
                                    .Select(x => x.Question.QuizId)
                                    .FirstOrDefaultAsync();

                        applied.QuizId = quizID;
                    }


                    await _context.AppliedPosts.AddAsync(applied);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    if (request.QuizSubmit != null)
                    {
                        List<UserAnswer> ls = new();

                        foreach (var item in request.QuizSubmit.AnswerId)
                        {
                            UserAnswer answer = new()
                            {
                                AnswerID = item,
                                BuilderId = user.BuilderId.Value
                            };

                            ls.Add(answer);

                        }
                        await _context.AddRangeAsync(ls);
                        await _context.SaveChangesAsync();

                    }

                    AppliedPost applied = new()
                    {
                        BuilderID = user.BuilderId.Value,
                        PostID = request.PostId,
                        WishSalary = request.WishSalary,
                        Status = Status.NOT_RESPONSE,
                        AppliedDate = DateTime.Now,
                        Video = request.Video
                    };

                    if (request.QuizSubmit != null)
                    {
                        var quizID = await _context.Answers
                                    .Include(x => x.Question)
                                    .Where(x => x.Id == request.QuizSubmit.AnswerId.First())
                                    .Select(x => x.Question.QuizId)
                                    .FirstOrDefaultAsync();

                        applied.QuizId = quizID;
                    }

                    await _context.AppliedPosts.AddAsync(applied);
                    await _context.SaveChangesAsync();
                }

                var postAuthor = await _context.ContractorPosts.Where(x => x.Id == request.PostId).Select(x => x.CreateBy).FirstOrDefaultAsync();
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = postAuthor.ToString(),
                    NavigateId = request.PostId

                };

                return response;
            }


        }

        public async Task<BasePagination<List<AppliedPostAll>>> ViewAllPostApplied(Guid userID, PaginationFilter filter)
        {
            BasePagination<List<AppliedPostAll>> response;
            List<AppliedPostAll> ls = new();
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
                filter._sortBy = "PostID";
            }


            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userID));
            if (user == null)
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.NOTFOUND_MESSAGE,
                };
                return response;
            }
            var rs = await _context.AppliedPosts
                .Include(x => x.ContractorPosts)
                    .ThenInclude(x => x.Contractor)
                        .ThenInclude(x => x.User)
                .Include(x => x.Quiz)
                 .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                .Where(x => x.BuilderID == user.BuilderId)
                .ToListAsync();

            totalRecord = rs.Count;

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
                Data = MapListAppliedAllDTO(rs, user.BuilderId.Value),
                Message = BaseCode.SUCCESS_MESSAGE,
                Pagination = pagination
            };
            return response;
        }

        private List<AppliedPostAll> MapListAppliedAllDTO(List<AppliedPost> list, int builderID)
        {
            List<AppliedPostAll> result = new();

            var userId = _context.Users.Where(x => x.BuilderId == builderID).Select(x=>x.Id).FirstOrDefault();

        


            foreach (var item in list)
            {
                var hasGroup = _context.Groups.Where(x => x.BuilderID == builderID && x.PostID == item.PostID).FirstOrDefault();
                List<CommitmentGroup> ls = new();

                if (hasGroup != null)
                {
                    var groupMember = _context.GroupMembers
                        .Where(x => x.GroupId == hasGroup.Id)
                        .ToList();

                    foreach (var member in groupMember)
                    {
                        var type = _context.Types.Where(x => x.Id == member.TypeID).Select(x => x.Name).FirstOrDefault();

                        CommitmentGroup group = new()
                        {
                            DOB = member.DOB,
                            Name = member.Name,
                            TypeID = member.TypeID,
                            TypeName = type,
                            VerifyId = member.IdNumber
                        };
                        ls.Add(group);
                    }
                }


                AppliedPostAll dto = new()
                {
                    PostId = item.PostID,
                    Avatar = item.ContractorPosts.Contractor?.User?.Avatar,
                    Description = item.ContractorPosts.Description,
                    EndDate = item.ContractorPosts.EndDate,
                    Place = item.ContractorPosts.Place,
                    ProjectName = item.ContractorPosts.ProjectName,
                    StarDate = item.ContractorPosts.StarDate,
                    AuthorName = item.ContractorPosts.Contractor?.User?.FirstName + " " + item.ContractorPosts.Contractor?.User?.LastName,
                    Title = item.ContractorPosts.Title,
                    AppliedDate = item.AppliedDate,
                    WishSalary = item.WishSalary,
                    Video = item.Video,
                    Status=item.ContractorPosts.Status,
                    Groups = ls.Any() ? ls : null,

                };

                var save = _context.Saves.Where(x => x.UserId.Equals(userId) && x.ContractorPostId == item.PostID).FirstOrDefault();
                if (save != null)
                {
                    dto.IsSave = true;
                }
                else
                {
                    dto.IsSave = false;
                }





                if (item.QuizId != null)
                {
                    List<QuizQuestionDTO> questionDTOs = new();
                    dto.QuizId = item.QuizId;
                    dto.QuizName = item.Quiz.Name;


                    var questionDB = _context.Questions.Where(x => x.QuizId == dto.QuizId).ToList();

                    foreach (var question in questionDB)
                    {
                        QuizQuestionDTO tmp = new()
                        {
                            QuestionId = question.Id,
                            QuestionName = question.Name,
                            Answers = MapListAnswerDTO(question.Id, builderID),

                        };
                        questionDTOs.Add(tmp);

                    }

                    dto.Questions = questionDTOs;

                }
                result.Add(dto);
            }
            return result;
        }

        public List<QuizAnswerDTO> MapListAnswerDTO(int questionID, int builderId)
        {
            var listAnswers = _context.Answers.Where(x => x.QuestionId == questionID).ToList();



            var result = new List<QuizAnswerDTO>();

            foreach (var item in listAnswers)
            {
                var userAnswerOfQuestion = _context.UserAnswers
                    .Include(x => x.Answer)
                    .Where(x => x.BuilderId == builderId && x.Answer.QuestionId == item.QuestionId)
                    .Select(x => x.AnswerID)
                    .FirstOrDefault();

                var answer = new QuizAnswerDTO()
                {
                    AnswerId = item.Id,
                    AnswerName = item.Name,
                    IsCorrect = item.isCorrect,
                    Answer = userAnswerOfQuestion
                };
                result.Add(answer);
            }
            return result;
        }








        public async Task<BasePagination<List<ContractorPostDTO>>> GetPostByContractor(PaginationFilter filter, Guid id)
        {
            BasePagination<List<ContractorPostDTO>> response;
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
                filter._sortBy = "LastModifiedAt";
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));

            IQueryable<ContractorPost> query = _context.ContractorPosts;
            StringBuilder salariesSearch = new();
            StringBuilder placeSearch = new();
            StringBuilder categoriesSearch = new();

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

            }

            var result = await query
                    .Include(x => x.Contractor)
                        .ThenInclude(x => x.User)
                     .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .Where(x => x.ContractorID == user.ContractorId)
                     .ToListAsync();


            if (filter.FilterRequest != null)
            {
                totalRecord = result.Count;
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
                    Data = MapListDTO(result, id),
                    Pagination = pagination
                };
            }
            return response;
        }

        public async Task<BaseResponse<QuizSubmitDetailDTO>> ViewDetailQuizSubmit(int quizId, int builderId)
        {
            BaseResponse<QuizSubmitDetailDTO> response = new();
            List<QuizQuestionDTO> questionDTOs = new();


            var quiz = await _context.Quizzes.FirstOrDefaultAsync(x => x.Id == quizId);

            if (quiz != null)
            {
                var questionDB = await _context.Questions.Where(x => x.QuizId == quiz.Id).ToListAsync();

                foreach (var question in questionDB)
                {
                    QuizQuestionDTO tmp = new()
                    {
                        QuestionId = question.Id,
                        QuestionName = question.Name,
                        Answers = MapListAnswerDTO(question.Id, builderId),

                    };
                    questionDTOs.Add(tmp);
                }

                QuizSubmitDetailDTO quizSubmitDetailDTO = new()
                {
                    QuizId = quiz.Id,
                    QuizName = quiz.Name,
                    TypeId = quiz.TypeID,
                    Questions = questionDTOs
                };


                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = quizSubmitDetailDTO
                };

            }
            else
            {

                response = new()
                {
                    Code = BaseCode.ERROR,
                    Message = BaseCode.NOTFOUND_MESSAGE
                };
            }
            return response;
        }

        public async Task<BasePagination<List<ContractorPostDTO>>> GetPostByCategories(PaginationFilter filter, Guid id, PostCategories categories)
        {
            BasePagination<List<ContractorPostDTO>> response;
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
                filter._sortBy = "LastModifiedAt";
            }



            IQueryable<ContractorPost> query = _context.ContractorPosts;

            var result = await query
                    .Include(x => x.Contractor)
                        .ThenInclude(x => x.User)
                    .Where(x => x.Status == Status.SUCCESS && x.PostCategories == categories)
                     .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .ToListAsync();


            if (filter.FilterRequest != null)
            {
                totalRecord = result.Count;
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
                    Data = MapListDTO(result, id),
                    Pagination = pagination
                };
            }
            return response;
        }

        public async Task<BaseResponse<bool>> ViewPostAppliedCheck(int builderId, Guid contractorId)
        {
            BaseResponse<bool> response;

            var ctor = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(contractorId));

            var allPost = await _context.ContractorPosts.Where(x => x.ContractorID == ctor.ContractorId && x.Status == Status.SUCCESS).ToListAsync();

            foreach (var item in allPost)
            {
                var check = _context.AppliedPosts.Where(x => x.PostID == item.Id && x.BuilderID == builderId).FirstOrDefault();

                if (check != null)
                {
                    response = new()
                    {
                        Code = BaseCode.SUCCESS,
                        Data = true,
                        Message = BaseCode.SUCCESS_MESSAGE
                    };
                    return response;
                }
            }

            response = new()
            {
                Code = BaseCode.SUCCESS,
                Data = false,
                Message = BaseCode.SUCCESS_MESSAGE
            };
            return response;
        }

        public async Task<BaseResponse<string>> UpdatePostStatus(int postId)
        {
            BaseResponse<string> response;

            var post = await _context.ContractorPosts.FirstOrDefaultAsync(x => x.Id == postId);

            if (post != null)
            {
                post.Status = Status.CANCEL;
                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = "Cập nhật thành công"
            };
            return response;
        }

        public async Task<BaseResponse<string>> UpdatePost(ContractorPostUpdate request)
        {
            BaseResponse<string> response = new();
            var postId = request.PostId;
            var query = await _context.ContractorPosts.Where(x => x.Id == postId).FirstOrDefaultAsync();
            var PostType = query?.ContractorPostTypes;
            var PostSkill = query?.PostSkills;
            var checkApplied = await _context.AppliedPosts.Where(x => x.PostID == postId).AnyAsync();
            var checkCommitment = await _context.PostCommitments.Where(x => x.PostID == postId).AnyAsync();
            if (checkApplied || checkCommitment)
            {
                response.Code = BaseCode.ERROR;
                response.Message = "Không thể chỉnh sửa bài post này";
            }
            else
            {
                if (!string.IsNullOrEmpty(request.Title))
                {
                    query.Title = request.Title;
                }
                if (!string.IsNullOrEmpty(request.ProjectName))
                {
                    query.ProjectName = request.ProjectName;
                }
                if (!string.IsNullOrEmpty(request.Description))
                {
                    query.Description = request.Description;
                }
                if (!string.IsNullOrEmpty(request.Required))
                {
                    query.Required = request.Required;
                }
                if (!string.IsNullOrEmpty(request.Benefit))
                {
                    query.Benefit = request.Benefit;
                }
                if (!string.IsNullOrEmpty(request.StarDate.ToString()))
                {
                    query.StarDate = request.StarDate;
                }
                if (!string.IsNullOrEmpty(request.EndDate.ToString()))
                {
                    query.EndDate = request.EndDate;
                }
                if (!string.IsNullOrEmpty(request.EndTime))
                {
                    query.EndTime = request.EndTime;
                }
                if (!string.IsNullOrEmpty(request.StartTime))
                {
                    query.StartTime = request.StartTime;
                }
                if (!string.IsNullOrEmpty(request.Salaries))
                {
                    query.Salaries = request.Salaries;
                }
                if (!string.IsNullOrEmpty(request.Accommodation.ToString()))
                {
                    query.Accommodation = request.Accommodation;
                }
                if (!string.IsNullOrEmpty(request.Place.ToString()))
                {
                    query.Place = request.Place;
                }
                if (!string.IsNullOrEmpty(request.Transport.ToString()))
                {
                    query.Transport = request.Transport;
                }
                if (!string.IsNullOrEmpty(request.NumberPeople.ToString()))
                {
                    query.NumberPeople = request.NumberPeople;
                }
                if (!string.IsNullOrEmpty(request.PostCategories.ToString()))
                {
                    query.PostCategories = request.PostCategories;
                }
                if (!string.IsNullOrEmpty(request.PostCategories.ToString()))
                {
                    query.PostCategories = request.PostCategories;
                }
                if (request.type != null)
                {
                    foreach (var item in request.type)
                    {
                        _context.Remove(query.ContractorPostTypes);
                        _context.SaveChanges();
                        var rType = new Data.Entities.ContractorPostType();
                        if (item.id != null)
                        {
                            rType.TypeID = (Guid)item.id;
                            rType.ContractorPostID = (int)postId;
                            _context.ContractorPostTypes.Add(rType);

                            var rs = _context.SaveChanges();
                            if (rs < 0)
                            {
                                response.Code = BaseCode.ERROR;
                                response.Message = "Cập nhật bài post không thành công";
                                return response;
                            }
                        }
                    }

                    var flag = false;
                    foreach (var i in request.type)
                    {
                        foreach (var o in i.SkillArr)
                        {
                            if (o.fromSystem == false)
                            {
                                var rSkill = new Skill
                                {
                                    Name = o.name,
                                    FromSystem = o.fromSystem
                                };
                                _context.Skills.Add(rSkill);
                                _context.SaveChanges();
                                var cPostSkill = new ContractorPostSkill
                                {
                                    ContractorPostID = (int)postId,
                                    SkillID = rSkill.Id
                                };
                                _context.ContractorPostSkills.Add(cPostSkill);
                                var rs = _context.SaveChanges();
                                if (rs < 0)
                                {
                                    response.Code = BaseCode.ERROR;
                                    response.Message = "Cập nhật bài post không thành công";
                                    return response;
                                }
                                flag = true;
                            }
                            else
                            {
                                var rs = await _context.Skills.Include(x => x.Type).Where(x => x.Id == o.id).ToListAsync();
                                foreach (var c in rs)
                                {
                                    var guid = i.id.ToString();
                                    var cid = c.TypeId.ToString();
                                    if (guid.Equals(cid))
                                    {

                                        var cPostSkill = new ContractorPostSkill
                                        {
                                            ContractorPostID = (int)postId,
                                            SkillID = o.id
                                        };
                                        _context.ContractorPostSkills.Add(cPostSkill);
                                        _context.SaveChanges();
                                        flag = true;
                                    }
                                    else
                                    {
                                        response.Code = BaseCode.ERROR;
                                        response.Message = "Cập nhật bài post không thành công";
                                        return response;
                                    }
                                }
                            }
                        }

                    };
                    await _context.SaveChangesAsync();
                    if (flag)
                    {
                        if (PostSkill != null)
                        {
                            _context.ContractorPostSkills.RemoveRange(PostSkill);
                            _context.SaveChanges();
                        }
                        if (PostType != null)
                        {
                            _context.ContractorPostTypes.RemoveRange(PostType);
                            _context.SaveChanges();
                        }

                    }
                }
                var update = _context.ContractorPosts.Update(query);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    response.Code = BaseCode.SUCCESS;
                    response.Message = "Cập nhật bài viết thành công";
                }
                else
                {
                    response.Code = BaseCode.ERROR;
                    response.Message = "Cập nhật bài viết không thành công";
                }
            }
            return response;
        }

        public async Task<BasePagination<List<ContractorPostDTO>>> GetPostByUserId(PaginationFilter filter, Guid id)
        {
            BasePagination<List<ContractorPostDTO>> response;
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
                filter._sortBy = "LastModifiedAt";
            }



            IQueryable<ContractorPost> query = _context.ContractorPosts;
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
                    .Where(x => x.Status == Status.SUCCESS && x.CreateBy.Equals(id))
                     .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .ToListAsync();


            if (filter.FilterRequest != null)
            {
                totalRecord = result.Count;
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
                    Data = MapListDTO(result, id),
                    Pagination = pagination
                };
            }
            return response;
        }
    }
}