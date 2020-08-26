using MediatR;
using QuizMaster.RequestsAndResponses.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMaster.RequestsAndResponses.HostQuiz
{
    public class HostQuizRequest : IRequest<HostQuizResponse>
    {
        public string Username { get; set; }
        public Difficulties Difficulty { get; set; }
    }
}
