using QuizMaster.RequestsAndResponses.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMaster.RequestsAndResponses.Models
{
    public class QuizQuestionDto
    {
        public Categories Category { get; set; }
        public QuestionTypes Type { get; set; }
        public Difficulties Difficulty { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public IList<string> IncorrectAnswers { get; set; }
    }
}
