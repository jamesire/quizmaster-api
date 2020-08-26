using MediatR;
using QuizMaster.RequestsAndResponses.HostQuiz;
using QuizMaster.Storage.ActiveQuizzesTable;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace QuizMaster.Domain.Handlers
{
    public class DeleteQuizHandler : IRequestHandler<DeleteQuizRequest, DeleteQuizResponse>
    {
        private readonly IActiveQuizzesTable _activeQuizzesTable;

        public DeleteQuizHandler(IActiveQuizzesTable activeQuizzesTable)
        {
            _activeQuizzesTable = activeQuizzesTable;
        }

        public async Task<DeleteQuizResponse> Handle(DeleteQuizRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteQuizResponse();

            try
            {
                await _activeQuizzesTable.DeleteQuiz(request.QuizId);
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
