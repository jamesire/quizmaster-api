using QuizMaster.RequestsAndResponses.Enums;
using System.Collections.Generic;

namespace QuizMaster.Domain.Models
{
    public class QuizQuestion
    {
        public Categories Category { get; set; }
        public QuestionTypes Type { get; set; }
        public string Question { get; set; }
        public IList<string> Answers { get; set; }
    }
}
