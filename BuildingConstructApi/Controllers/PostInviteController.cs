using Application.System.Notifies;
using Application.System.PostInvite;
using BuildingConstructApi.Hubs;
using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ViewModels.Notificate;
using ViewModels.Pagination;
using ViewModels.PostInvite;

namespace BuildingConstructApi.Controllers
{
    [Route("api/invite")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostInviteController : ControllerBase
    {
        private readonly IPostInviteService postInviteService;
        private readonly BuildingConstructDbContext _context;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;


        public PostInviteController(IPostInviteService postInviteService, BuildingConstructDbContext context, IUserConnectionManager userConnectionManager, IHubContext<NotificationUserHub> notificationUserHubContext)
        {
            this.postInviteService = postInviteService;
            _context = context;
            _userConnectionManager = userConnectionManager;
            _notificationUserHubContext = notificationUserHubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter request)
        {
            var userID = User.FindFirst("UserID").Value;
            var result = await postInviteService.GetAll(request, Guid.Parse(userID));
            return Ok(result);


        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostIniviteRequest requests)
        {
            var result = await postInviteService.Create(requests);

            var author = await _context.Users.Where(x => x.ContractorId==requests.ContractorId).FirstOrDefaultAsync();

            NotificateAuthor notiAuthor = new()
            {
                Avatar = author.Avatar,
                FirstName = author.FirstName,
                LastName = author.LastName,
            };

            NotificationModels noti = new() 
            {
                LastModifiedAt = DateTime.Now,
                CreateBy =author.Id,
                NavigateId = requests.ContractorPostId,
                UserId = Guid.Parse(result.Data),
                Message = NotificationMessage.SEND_INVITE,
                NotificationType = NotificationType.CONTRACTOR_POST_NOTIFICATION,
                Author = notiAuthor,
            };

            var check = await _userConnectionManager.SaveNotification(noti);
            var connections = _userConnectionManager.GetUserConnections(result.Data.ToString());
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
            return Ok(result);


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {

            var result = await postInviteService.Update(id);
            return Ok(result);
        }

        [HttpGet("isInvite")]
        public async Task<IActionResult> IsInvite([FromQuery] int builderID, [FromQuery] int postID)
        {
            var result = await postInviteService.isInvite(builderID, postID);
            return Ok(result);
        }


        [HttpGet("checkInvite")]
        public async Task<IActionResult> CheckInvite([FromQuery] int builderID)
        {
            var userID = User.FindFirst("UserID")?.Value;


            var result = await postInviteService.CheckInvite(builderID, Guid.Parse(userID));
            return Ok(result);
        }


    }
}
