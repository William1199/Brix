using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class ResetPasswordDTO
    {
        public string PhoneNumber { get; set; }
        public string NewPassword { get; set; }
    }
}
