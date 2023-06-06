using Application.System.Commitments;
using Application.System.Notifies;
using BuildingConstructApi.Hubs;
using Data.DataContext;
using Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ViewModels.Commitment;
using ViewModels.Notificate;
using ViewModels.Pagination;

namespace BuildingConstructApi.Controllers
{
    [Route("api/commitment")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CommitmentController : ControllerBase
    {
        private readonly ICommitmentService _commitmentService;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly BuildingConstructDbContext _context;

        public CommitmentController(ICommitmentService commitmentService, IUserConnectionManager userConnectionManager, BuildingConstructDbContext context, IHubContext<NotificationUserHub> notificationUserHubContext)
        {
            _commitmentService = commitmentService;
            _userConnectionManager = userConnectionManager;
            _context = context;
            _notificationUserHubContext = notificationUserHubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter request, Status status)
        {
            var validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            string? userID = User.FindFirst("UserID")?.Value; ;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _commitmentService.GetCommitment(Guid.Parse(userID), validFilter, status);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            string? userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _commitmentService.GetDetailCommitment(id);
            return Ok(result);
        }
        [HttpGet("load")]
        public async Task<IActionResult> GetData(int postID, int builderID)
        {
            string? userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _commitmentService.GetDetailForCreate(postID, builderID, Guid.Parse(userID));
            return Ok(result);
        }

        [HttpGet("builder/{id}")]
        public async Task<IActionResult> GetPostWithCommitment([FromQuery] PaginationFilter request, [FromRoute] int id)
        {
            var result = await _commitmentService.GetPost(request, id);
            return Ok(result);
        }



        [HttpPut("{commitmentID}")]
        public async Task<IActionResult> UpdateCommitment([FromRoute] int commitmentID)
        {
            string? userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _commitmentService.UpdateCommitment(Guid.Parse(userID), commitmentID);

            var author = await _context.Users.Where(x => x.Id.ToString().Equals(userID.ToString())).FirstOrDefaultAsync();

            NotificateAuthor notiAuthor;

            if (author != null)
            {
                notiAuthor = new()
                {
                    Avatar = author.Avatar,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                };

                NotificationModels noti = new()
                {
                    LastModifiedAt = DateTime.Now,
                    CreateBy = Guid.Parse(userID),
                    NavigateId = result.NavigateId,
                    UserId = Guid.Parse(result.Data.ToString()),
                    Message = NotificationMessage.UPDATE_COMMIMENT,
                    NotificationType = NotificationType.COMMITMENT_NOTIFICATION,
                    Author = notiAuthor,
                };

                var check = await _userConnectionManager.SaveNotification(noti);
                var connections = _userConnectionManager.GetUserConnections(result.Data);
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
            }
            return Ok(result);
        }
        [HttpGet("getTop5Commitment")]
        public async Task<IActionResult> GetTop5CommitmentContractor()
        {
            var result = await _commitmentService.GetTop5CommitmentContractor();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCommitment([FromBody] CreateCommimentRequest request)
        {
            string? contractorID = User.FindFirst("UserID")?.Value;

            if (contractorID == null)
            {
                return BadRequest();
            }

            var result = await _commitmentService.CreateCommitment(request, Guid.Parse(contractorID));


            if (result.Data.Equals("ALREADY_COMMITMENT"))
            {
                return Ok(result);
            }

            if (result.Data.Equals("ALREADY_CREATE_COMMITMENT"))
            {
                return Ok(result);
            }




            var connections = _userConnectionManager.GetUserConnections(result.Data);



            NotificationModels noti = new()
            {
                NotificationType = NotificationType.COMMITMENT_NOTIFICATION,
                Message = NotificationMessage.COMMITMENTNOTI,
                CreateBy = Guid.Parse(contractorID.ToString()),
                UserId = Guid.Parse(result.Data.ToString()),
                LastModifiedAt = DateTime.Now,
                NavigateId = result.NavigateId
            };
            var author = await _context.Users.Where(x => x.Id.ToString().Equals(noti.CreateBy.ToString())).FirstOrDefaultAsync();
            if (author != null)
            {
                noti.Author = new()
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Avatar = author.Avatar
                };
                var check = await _userConnectionManager.SaveNotification(noti);
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
            else
            {
                return BadRequest();
            }

        }
    }
}


