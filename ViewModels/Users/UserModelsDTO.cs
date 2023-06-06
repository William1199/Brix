using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class UserModelsDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string RoleName { get; set; }
        public string? Avatar { get; set; }
    }
}
