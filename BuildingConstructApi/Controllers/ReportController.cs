using Application.System.Notifies;
using Application.System.Reports;
using BuildingConstructApi.Hubs;
using Data.DataContext;
using Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ViewModels.MaterialStore;
using ViewModels.Notificate;
using ViewModels.Pagination;

namespace BuildingConstructApi.Controllers
{
    [Route("api/report")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly BuildingConstructDbContext _context;

        public ReportController(IReportService reportService, IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, BuildingConstructDbContext context)
        {
            _reportService = reportService;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _context = context;
        }


        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllReport([FromBody] PaginationFilter request)
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

            var result = await _reportService.GetAllReportProduct(request);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportRequestDTO request)
        {
            var rs = await _reportService.ReportProduct(request);
            NotificationModels noti = new();
            noti.NotificationType = NotificationType.CREATEREPORT;
            noti.Message = NotificationMessage.REPORT_PRODUCT;
            var userID = User.FindFirst("UserID")?.Value;
            noti.CreateBy = Guid.Parse(userID.ToString());
            noti.UserId = Guid.Parse(rs.Data);
            var author = await _context.Users.Where(x => x.Id.ToString().Equals(userID.ToString())).FirstOrDefaultAsync();
            noti.Author = new();
            noti.Author.FirstName = author.FirstName;
            noti.Author.LastName = author.LastName;
            noti.Author.Avatar = author.Avatar;
            noti.LastModifiedAt = DateTime.Now;
            noti.NavigateId = rs.NavigateId;
            if (rs.Data != null)
            {
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
            }
            return Ok(rs);
        }
        [HttpPost("createReportPost")]
        public async Task<IActionResult> CreatePostReport([FromBody] ReportRequestDTO request)
        {

            var rs = await _reportService.ReportPost(request);
            NotificationModels noti = new();
            noti.NotificationType = NotificationType.CONTRACTOR_POST_NOTIFICATION;
            noti.Message = NotificationMessage.REPORT_POST;
            var userID = User.FindFirst("UserID")?.Value;
            noti.CreateBy = Guid.Parse(userID.ToString());
            noti.UserId = Guid.Parse(rs.Data);
            var author = await _context.Users.Where(x => x.Id.ToString().Equals(userID.ToString())).FirstOrDefaultAsync();
            noti.Author = new();
            noti.Author.FirstName = author.FirstName;
            noti.Author.LastName = author.LastName;
            noti.Author.Avatar = author.Avatar;
            noti.LastModifiedAt = DateTime.Now;
            noti.NavigateId = rs.NavigateId;
            if (rs.Data != null)
            {
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
            }
            return Ok(rs);
        }
        [HttpPost("getAllPostReport")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPostReport([FromBody] PaginationFilter request)
        {
            var validFilter = new PaginationFilter();
            var id = User.FindFirst("UserID").Value;

            if (request.FilterRequest == null)
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            }
            else
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy, request.FilterRequest);

            }

            var result = await _reportService.GetAllReportPost(request);
            return Ok(result);
        }

    }
}
