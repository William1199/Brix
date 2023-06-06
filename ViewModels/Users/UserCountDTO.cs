using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class UserCountDTO
    {
        public int TotalUser { get; set; }
        public int Worker { get; set; }
        public int MaterialStore { get; set; }
        public int Contractor { get; set; }
    }
}
