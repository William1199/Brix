using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Answer
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public bool isCorrect { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public List<UserAnswer> UserAnswers { get; set; }
    }
}
