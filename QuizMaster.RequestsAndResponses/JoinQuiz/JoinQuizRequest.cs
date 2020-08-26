using MediatR;
using QuizMaster.RequestsAndResponses.JoinQuiz;

namespace QuizMaster.RequestsAndResponses
{
    public class JoinQuizRequest : IRequest<JoinQuizResponse>
    {
        public string QuizId { get; set; }
        public string Username { get; set; }
    }
}
