using Amazon.Runtime.Internal.Util;
using MediatR;
using QuizMaster.RequestsAndResponses.Enums;
using QuizMaster.RequestsAndResponses.HostQuiz;
using QuizMaster.Storage.ActiveQuizzesTable;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace QuizMaster.Domain.Handlers
{
    public class HostQuizHandler : IRequestHandler<HostQuizRequest, HostQuizResponse>
    {
        private readonly IActiveQuizzesTable _activeQuizzesTable;

        public HostQuizHandler(IActiveQuizzesTable activeQuizzesTable)
        {
            _activeQuizzesTable = activeQuizzesTable;
        }

        public async Task<HostQuizResponse> Handle(HostQuizRequest request, CancellationToken cancellationToken)
        {
            // Generate a string of 6 random letters
            // Add this string of letters to dynamodb table
            // Did this work? Return success
            // Didn't work? Return failure
            string password = RandomString(6);
            var response = new HostQuizResponse();

            var difficulty = request.Difficulty;
            if (difficulty == Difficulties.Any)
            {
                difficulty = GenerateRandomDifficulty(request.Difficulty);
            }

            try
            {
                await _activeQuizzesTable.AddQuiz(password, request.Username, (QuizMaster.Storage.Enums.Difficulties)difficulty);
                response.StatusCode = HttpStatusCode.OK;
                response.QuizId = password;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = e.ToString();
            }

            return response;
        }

        private Difficulties GenerateRandomDifficulty(Difficulties difficulty)
        {
            var randomDifficulty = (Difficulties)new Random().Next(1, 4);

            return randomDifficulty;
        }

        private string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
