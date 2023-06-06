using Data.Entities;
using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PostCommitment
    {
        public int Id { get; set; } 

        public int? PostID { get; set; }

        public int? BuilderID { get; set; }

        public int? ContractorID { get; set; }

        public int? GroupID { get; set; }

        public Builder Builder { get; set; }    

        public Contractor Contractor { get; set; }  

        public Group Group { get; set; }

        public ContractorPost ContractorPosts { get; set; }


        public string? OptionalTerm { get; set; }
        public string? Salaries { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }



        //public Guid UserID { get; set; }

        //public int CommitmentID { get; set; }



        //public Commitment Commitment { get; set; }

        //public User User { get; set; }

        public Status Status { get; set; }


    }
}


