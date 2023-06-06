using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Notificate
{
    public class NotificationModels
    {
        public int? Id { get; set; }
        public NotificationType NotificationType { get; set; }
        public string? Message { get; set; }
        public int? NavigateId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;

        public Guid CreateBy { get; set; }
        public NotificateAuthor Author { get; set; }
    }
}
