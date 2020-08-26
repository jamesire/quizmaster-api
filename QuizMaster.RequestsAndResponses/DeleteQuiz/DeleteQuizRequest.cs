using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMaster.RequestsAndResponses.HostQuiz
{
    public class DeleteQuizRequest : IRequest<DeleteQuizResponse>
    {
        public string QuizId { get; set; }
    }
}
