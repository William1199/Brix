using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Claims;
using ViewModels.BuilderPosts;
using ViewModels.ContractorPost;
using ViewModels.Pagination;
using ViewModels.Response;
using ViewModels.SavePost;
using ViewModels.Users;

namespace Application.System.SavePost
{
    public class SaveService : ISaveService
    {
        private readonly BuildingConstructDbContext _context;
        private IHttpContextAccessor _accessor;

        public SaveService(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<BasePagination<List<SavePostDetailDTO>>> GetSavePostByUsID(PaginationFilter filter)
        {
            BasePagination<List<SavePostDetailDTO>> response = new();
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");

            List<SavePostDetailDTO> list = new();
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
            if (identifierClaim == null)
            {
                response.Message = BaseCode.ERROR_MESSAGE;
                response.Code = "202";
                return response;
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString().Equals(identifierClaim.Value.ToString()));
            if (user == null)
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.NOTFOUND_MESSAGE,
                };
                return response;
            }
            var rs = await _context.Saves.Include(x => x.ContractorPost).Where(x => x.UserId.ToString().Equals(user.Id.ToString()))
                 .OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
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

            if (rs != null)
            {
                foreach (var item in rs)
                {
                    list.Add(await MapToDTO(item));
                }
                response.Data = list;
                response.Pagination = pagination;
                response.Message = BaseCode.SUCCESS;
                response.Code = BaseCode.SUCCESS_MESSAGE;
            }
            else
            {
                response.Message = BaseCode.ERROR_MESSAGE;
                response.Code = BaseCode.ERROR;
            }
            return response;
        }
        private async Task<SavePostDetailDTO> MapToDTO(Save save)
        {
            SavePostDetailDTO dto = new();
            if (dto.ContractorPost != null)
            {
                dto.UserId = save.UserId;
                dto.ContractorPost = await GetContractorPost(save.ContractorPost, save.UserId);
                dto.CreatedDate = save.Date;
            }
            else
            {
                dto.UserId = save.UserId;
                dto.ContractorPost = await GetContractorPost(save.ContractorPost, save.UserId);
                dto.CreatedDate = save.Date;
            }
            return dto;
        }
        private async Task<List<ContractorPostDetailDTO>> GetContractorPost(ContractorPost post,Guid userId)
        {
            var results = await _context.ContractorPosts.Where(x => x.Id == post.Id).ToListAsync();

            var final = new List<ContractorPostDetailDTO>();
            ContractorPostDetailDTO dto = new();

            foreach (var x in results)
            {
                dto.Id = x.Id;
                dto.Title = x.Title;
                dto.ProjectName = x.ProjectName;
                dto.Description = x.Description;
                dto.Required = x.Required;
                dto.NumberPeople = x.NumberPeople;
                dto.Benefit = x.Benefit;
                dto.Required = x.Required;
                dto.type = await GetTypeAndSkillFromPost(post.Id);
                dto.Salaries = x.Salaries;
                dto.LastModifiedAt = x.LastModifiedAt;
                dto.PeopleRemained = x.PeopeRemained;
                dto.PostCategories = x.PostCategories;
                dto.Place = x.Place;
                dto.StarDate = x.StarDate;
                dto.EndDate = x.EndDate;
                dto.Author = await GetUserProfile(post.CreateBy);
                dto.Status = x.Status;

                var save = _context.Saves.Where(x => x.UserId.Equals(userId) && x.ContractorPostId == x.Id).FirstOrDefault();
                if (save != null)
                {
                    dto.IsSave = true;
                }
                else
                {
                    dto.IsSave = false;
                }


                final.Add(dto);
            }
            return final;
        }
        private async Task<List<TypeModels>> GetTypeAndSkillFromPost(int postID)
        {
            var results = await _context.ContractorPostTypes.Include(x => x.Type).Where(x => x.ContractorPostID == postID).ToListAsync();
            var rsSkill = await _context.ContractorPostSkills.Include(x => x.Skills).Where(x => x.ContractorPostID == postID).ToListAsync();
            var final = new List<TypeModels>();
            foreach (var item in results)
            {
                var type = new TypeModels();
                type.id = item.TypeID;
                type.name = item.Type.Name;
                type.SkillArr = new();
                foreach (var i in rsSkill)
                {
                    if (i.Skills.TypeId.Equals(item.TypeID))
                    {
                        var skillArr = new SkillArr();
                        skillArr.id = i.SkillID;
                        skillArr.name = i.Skills.Name;
                        skillArr.fromSystem = i.Skills.FromSystem;
                        skillArr.TypeId = i.Skills.TypeId;
                        type.SkillArr.Add(skillArr);
                    }
                }
                final.Add(type);
            }
            if (rsSkill.Where(x => x.Skills.TypeId == null).ToList().Any())
            {
                var t = new TypeModels();
                foreach (var i in rsSkill.Where(x => x.Skills.TypeId == null).ToList())
                {
                    t.SkillArr = new();
                    var skillArr = new SkillArr();
                    skillArr.id = i.SkillID;
                    skillArr.name = i.Skills.Name;
                    skillArr.fromSystem = i.Skills.FromSystem;
                    skillArr.TypeId = i.Skills.TypeId;
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
            var final = new UserModelsDTO();
            final.UserName = results.UserName;
            final.Phone = results.PhoneNumber;
            final.FirstName = results.FirstName;
            final.LastName = results.LastName;
            final.Address = results.Address;
            final.Gender = results.Gender;
            final.Avatar = results.Avatar;
            final.DOB = results.DOB;
            final.Email = results.Email;
            final.RoleName = await _context.Roles.Where(x => x.Id.Equals(roleid)).Select(x => x.Name).SingleOrDefaultAsync();
            return final;
        }

        public async Task<BaseResponse<string>> SavePost(SavePostRequest request)
        {
            BasePagination<string> response = new();
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            if (identifierClaim == null)
            {
                response.Message = BaseCode.ERROR_MESSAGE;
                response.Code = "202";
                return response;
            }
            var userID = identifierClaim.Value;
            Save save = new()
            {
                Date = DateTime.Now,
                ContractorPostId = request.ContractorPostId,
                UserId = Guid.Parse(userID)
            };
            var sendID = await _context.ContractorPosts.Where(x => x.Id == request.ContractorPostId).Select(x => x.CreateBy).FirstOrDefaultAsync();

            await _context.Saves.AddAsync(save);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Code = "200";
                response.Data = sendID.ToString();
                response.NavigateId = request.ContractorPostId;
            }
            else
            {
                response.Message = BaseCode.ERROR_MESSAGE;
                response.Code = "202";
            }
            return response;
        }

        public async Task<bool> DeleteSave(DeleteSaveRequest request)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            if (identifierClaim.Value == null)
            {
                return false;
            }
            var userID = identifierClaim.Value.ToString();
            dynamic save;

            save = await _context.Saves.FirstOrDefaultAsync(x => x.ContractorPostId == request.ContractorPostId && x.UserId.ToString().Equals(userID.ToString()));



            if (save == null)
            {
                return false;
            }
            _context.Saves.Remove(save);
            await _context.SaveChangesAsync();


            return true;
        }

        //public bool CompareFaces(Mat IDCardFace, Mat LiveFace)
        //{
        //    // Convert images to grayscale for processing
        //    IDCardFace = IDCardFace.ToImage<Gray, byte>().Mat;
        //    LiveFace = LiveFace.ToImage<Gray, byte>().Mat;

        //    // Detect faces in both images
        //    var IDCardFaceDetected = DetectFace(IDCardFace);
        //    var LiveFaceDetected = DetectFace(LiveFace);

        //    // If both faces were detected, compare them
        //    if (IDCardFaceDetected != null && LiveFaceDetected != null)
        //    {
        //        // Calculate Euclidean distance between the two face descriptors
        //        double distance = CvInvoke.Norm(IDCardFaceDetected, LiveFaceDetected, NormType.L2);

        //        // Return true if the distance is within a threshold, indicating a match
        //        return distance < 0.6;
        //    }

        //    // Return false if one or both faces were not detected
        //    return false;
        //}

        public BaseResponse<string> DetectFace(Mat image)
        {
            BaseResponse<string> response;
            // Load the face detection model
            using (var faceDetector = new CascadeClassifier("D:\\SPRING_2023 (Ky 9)\\Capstone\\BuildingConstruct\\BuildingConstructApplication\\System\\SavePost\\haarcascade_frontalface_default.xml"))
            {
                // Detect faces in the image
                Rectangle[] faces = faceDetector.DetectMultiScale(
                    image,
                    scaleFactor: 1.1,
                    minNeighbors: 10,
                    minSize: new Size(20, 20)
                );

                // If a face was detected, extract its descriptor
                if (faces.Length > 0)
                {
                    // Use the first detected face
                    Rectangle face = faces[0];

                    // Crop the face from the image
                    Mat faceImage = new Mat(image, face);

                    // Resize the face image to a standard size
                    CvInvoke.Resize(faceImage, faceImage, new Size(300, 300));

                    // Return the face descriptor
                    //return faceImage.ToImage<Gray, byte>().Mat;
                    response = new()
                    {
                        Code = BaseCode.SUCCESS,
                        Message = "Detected"
                    };
                    return response;
                }
            }

            // Return null if no face was detected
            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = "Not Detected"
            };
            return response;
        }

    }
}
