using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Notificate;
using ViewModels.Response;

namespace Application.System.Notifies
{
    public interface IUserConnectionManager
    {
        void KeepUserConnection(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(string userId);
        public Task<BaseResponse<Notification>> SaveNotification(NotificationModels noti);
    }
}
