using Data.Entities;
using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Commitment
{
    public class CreateCommimentRequest
    {
        public string? OptionalTerm { get; set; }
        public int PostContractorID { get; set; }
        public int BuilderID { get; set; }
        public string? Salaries { get; set; }

    }
}
