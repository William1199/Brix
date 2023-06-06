using Application.System.Notifies;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ViewModels.Notificate;
using Data.Entities;
using Data.DataContext;

namespace BuildingConstructApi.Hubs
{
    public class NotificationUserHub : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;
        private IHttpContextAccessor _accessor;


        public NotificationUserHub(IUserConnectionManager userConnectionManager, IHttpContextAccessor accessor)
        {
            _userConnectionManager = userConnectionManager;
            _accessor = accessor;
        }
        public async override Task OnConnectedAsync()
        {
            var identifierClaim = _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();
            if (identifierClaim == null)
            {
                return;
            }
            var userID = identifierClaim.ToString();
            _userConnectionManager.KeepUserConnection(userID, Context.ConnectionId);
            await Task.CompletedTask;
        }
        
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //get the connectionId
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);
            var value = await Task.FromResult(0);//adding dump code to follow the template of Hub > OnDisconnectedAsync
        }
    }
}
