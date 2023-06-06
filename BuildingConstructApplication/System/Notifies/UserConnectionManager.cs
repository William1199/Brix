using Data.DataContext;
using Data.Entities;
using System.Collections.Concurrent;
using ViewModels.Notificate;
using ViewModels.Response;

namespace Application.System.Notifies
{
    public class UserConnectionManager : IUserConnectionManager
    {
        private static ConcurrentDictionary<string, ConcurrentBag<string>> userConnectionMap = new ConcurrentDictionary<string, ConcurrentBag<string>>();

        private readonly ReaderWriterLockSlim userConnectionMapLock = new ReaderWriterLockSlim();

        private static string userConnectionMapLocker = string.Empty;
        private readonly BuildingConstructDbContext _context;

        public UserConnectionManager(BuildingConstructDbContext context)
        {
            _context = context;
        }

        public void KeepUserConnection(string userId, string connectionId)
        {
            var connections = userConnectionMap.GetOrAdd(userId, new ConcurrentBag<string>());
            connections.Add(connectionId);
        }

        public void RemoveUserConnection(string connectionId)
        {
            userConnectionMapLock.EnterWriteLock();
            try
            {
                foreach (var connections in userConnectionMap.Values)
                {
                    connections.TryTake(out connectionId);
                }
            }
            finally
            {
                userConnectionMapLock.ExitWriteLock();
            }
        }

        public List<string> GetUserConnections(string userId)
        {
            userConnectionMapLock.EnterReadLock();
            try
            {
                if (userConnectionMap.TryGetValue(userId, out ConcurrentBag<string> connections) != null)
                {
                    return new List<string>(connections);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                userConnectionMapLock.ExitReadLock();
            }

            //var conn = new List<string>();
            //lock (userConnectionMapLocker)
            //{
            //    List<string> ls = new();

            //    ls.Add(userId);

            //    conn = ls;
            //}
            //return conn;
        }




        public async Task<BaseResponse<Notification>> SaveNotification(NotificationModels noti)
        {
            BaseResponse<Notification> response = new();
            Notification notification = new Notification()
            {
                Title = "",
                UserID = (Guid)noti.UserId,
                CreateBy = noti.CreateBy,
                IsRead = false,
                LastModifiedAt = noti.LastModifiedAt,
                Message = noti.Message,
                Type = noti.NotificationType,
                NavigateId = noti.NavigateId
            };
            await _context.Notifcations.AddAsync(notification);
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Data = notification;
            }
            else
            {
                response.Data = null;

            }
            return response;


        }
    }
}
