using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class UpdateRoleRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleId { get; set; }
    }
}
