using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Notificate
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public int? NavigateId { get; set; }
        public string Title { get; set; }
        public NotificationType Type { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public Guid UserID { get; set; }
        public Guid CreateBy { get; set; }
        public NotificateAuthor Author { get; set; }
        public DateTime LastModifiedAt { get; set; }

    }
}
