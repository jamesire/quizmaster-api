using MediatR;
using QuizMaster.Domain.QuizQuestionClient;
using QuizMaster.RequestsAndResponses.GetRandomQuestion;
using QuizMaster.RequestsAndResponses.Models;
using QuizMaster.RequestsAndResponses.OpenTdb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace QuizMaster.Domain.Handlers
{
    public class GetRandomQuizQuestionHandler : IRequestHandler<GetRandomQuizQuestionRequest, GetRandomQuizQuestionResponse>
    {
        private readonly IOpenTdbClient _openTdbClient;

        public GetRandomQuizQuestionHandler(IOpenTdbClient openTdbClient)
        {
            _openTdbClient = openTdbClient;
        }

        public async Task<GetRandomQuizQuestionResponse> Handle(GetRandomQuizQuestionRequest request, CancellationToken cancellationToken)
        {
            var openTdbResponse = await _openTdbClient.GetRandomQuestion();

            var response = FormRandomQuizQuestionResponseFromOpenTdbResponse(openTdbResponse);

            return response;
        }

        private GetRandomQuizQuestionResponse FormRandomQuizQuestionResponseFromOpenTdbResponse(OpenTdbResponse openTdbResponse)
        {
            var randomQuestionResponse = new GetRandomQuizQuestionResponse();

            if(openTdbResponse.StatusCode == HttpStatusCode.OK)
            {
                randomQuestionResponse.QuizQuestion = openTdbResponse.QuizQuestions[0];
            }

            return randomQuestionResponse;
        }
    }
}
