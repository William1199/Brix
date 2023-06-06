using Application.System.Notifies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ViewModels.Pagination;

namespace BuildingConstructApi.Controllers
{
    [Route("api/notification")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationServices _notificationServices;
        public NotificationController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotification([FromQuery] PaginationFilter request)
        {
            var validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            var userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _notificationServices.GetAllNotification(request,Guid.Parse(userID));
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIsReadNotification([FromRoute] int id)
        {
            var rs = await _notificationServices.UpdateIsRead(id);

            return Ok(rs);
        }
    }
}
