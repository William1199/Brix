using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class UpdateContractorRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? Avatar { get; set; }
        public Status Status { get; set; }
        public string? IdNumber { get; set; }

        public string? Phone { get; set; }

        public string? CompanyName { get; set; }

        public string? Description { get; set; }

        public string? Website { get; set; }
    }
}
