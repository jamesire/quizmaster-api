using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using QuizMaster.RequestsAndResponses;
using QuizMaster.RequestsAndResponses.GetRandomQuestion;
using QuizMaster.RequestsAndResponses.HostQuiz;
using QuizMaster.RequestsAndResponses.JoinQuiz;
using QuizMaster.Storage.ActiveQuizzesTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMasterApi.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class QuizController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IActiveQuizzesTable _activeQuizzesTable;

        public QuizController(IMediator mediator, IActiveQuizzesTable activeQuizzesTable)
        {
            _mediator = mediator;
            _activeQuizzesTable = activeQuizzesTable;
        }

        [HttpGet("random")]
        public async Task<GetRandomQuizQuestionResponse> GetRandomQuestion()
        {
            var request = new GetRandomQuizQuestionRequest();
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpPost("hostQuiz")]
        public async Task<HostQuizResponse> HostQuiz([FromBody] HostQuizRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpPost("joinQuiz")]
        public async Task<JoinQuizResponse> JoinQuiz([FromBody] JoinQuizRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("isAlive")]
        public async Task<string> IsAlive()
        {
            return "Alive!";
        }
    }
}
