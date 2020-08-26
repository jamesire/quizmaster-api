using MediatR;
using QuizMaster.RequestsAndResponses;
using QuizMaster.RequestsAndResponses.HostQuiz;
using QuizMaster.RequestsAndResponses.JoinQuiz;
using QuizMaster.Storage.ActiveQuizzesTable;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace QuizMaster.Domain.Handlers
{
    public class JoinQuizHandler : IRequestHandler<JoinQuizRequest, JoinQuizResponse>
    {
        private readonly IActiveQuizzesTable _activeQuizzesTable;

        public JoinQuizHandler(IActiveQuizzesTable activeQuizzesTable)
        {
            _activeQuizzesTable = activeQuizzesTable;
        }

        public async Task<JoinQuizResponse> Handle(JoinQuizRequest request, CancellationToken cancellationToken)
        {
            var response = new JoinQuizResponse();

            try
            {
                await _activeQuizzesTable.JoinQuiz(request.QuizId, request.Username);
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = e.ToString();
            }

            return response;
        }
    }
}
