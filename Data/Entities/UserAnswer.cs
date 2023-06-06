using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserAnswer
    {
        public int BuilderId { get; set; }

        public Builder Builder { get; set; }

        public int AnswerID { get; set; }

        public Answer Answer { get; set; }


    }
}
