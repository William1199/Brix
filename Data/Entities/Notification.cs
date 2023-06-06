using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Notification:BaseEntity
    {
        public int Id { get; set; } 
        public int? NavigateId { get; set; }
        public string Title { get; set; }
        public NotificationType Type { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public Guid UserID { get; set; }

        public User User { get; set; }
    }
}
