using Application.System.Notifies;
using Application.System.SavePost;
using BuildingConstructApi.Hubs;
using Data.DataContext;
using Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ViewModels.Notificate;
using ViewModels.Pagination;
using ViewModels.Response;
using ViewModels.SavePost;

namespace BuildingConstructApi.Controllers
{
    [Route("api/savepost")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SavePostController : ControllerBase
    {
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly BuildingConstructDbContext _context;
        private readonly ISaveService _saveService;

        public SavePostController(ISaveService saveService, IUserConnectionManager userConnectionManager, IHubContext<NotificationUserHub> notificationUserHubContext, BuildingConstructDbContext context)
        {
            _saveService = saveService;
            _userConnectionManager = userConnectionManager;
            _notificationUserHubContext = notificationUserHubContext;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreateSavePost([FromBody] SavePostRequest request)
        {
            var rs = await _saveService.SavePost(request);
            NotificationModels noti = new();
            noti.NotificationType = NotificationType.CONTRACTOR_POST_NOTIFICATION;
            noti.Message = NotificationMessage.SAVENOTI;
            var userID = User.FindFirst("UserID").Value;
            noti.CreateBy = Guid.Parse(userID.ToString());
            noti.UserId = Guid.Parse(rs.Data.ToString());
            var author = await _context.Users.Where(x => x.Id.ToString().Equals(userID.ToString())).FirstOrDefaultAsync();
            noti.Author = new();
            noti.Author.FirstName = author.FirstName;
            noti.Author.LastName = author.LastName;
            noti.Author.Avatar = author.Avatar;
            noti.LastModifiedAt = DateTime.Now;
            noti.NavigateId = rs.NavigateId;
            var check = await _userConnectionManager.SaveNotification(noti);
            var connections = _userConnectionManager.GetUserConnections(rs.Data);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {

                    if (check != null)
                    {
                        noti.Id = check.Data.Id;
                        await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", noti);

                    }
                }
            }
            return Ok(rs);
        }
        [HttpGet("getAllSave")]
        public async Task<IActionResult> GetAllSavePost([FromQuery] PaginationFilter request)
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
            var rs = await _saveService.GetSavePostByUsID(validFilter);
            return Ok(rs);
        }
        [HttpPut]
        public async Task<IActionResult> DeleteSave([FromBody] DeleteSaveRequest request)
        {
            BaseResponse<string> response = new();
            var rs = await _saveService.DeleteSave(request);
            if (rs)
            {
                response.Code = "200";
                response.Message = "Delete repost succesfully";
            }
            else
            {
                response.Code = "202";
                response.Message = "Delete repost unsuccesfully";
            }
            return Ok(response);
        }



        //using (var memoryStream = new MemoryStream())
        //{
        //    await image[1].CopyToAsync(memoryStream);
        //    using (var img = Image.FromStream(memoryStream))
        //    {
        //        face = GetMatFromSDImage(img);

        //    }
        //}






    }
}
