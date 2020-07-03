using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using QuizMaster.RequestsAndResponses.GetRandomQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMasterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("random")]
        public async Task<GetRandomQuizQuestionResponse> GetRandomQuestion()
        {
            var request = new GetRandomQuizQuestionRequest();
            var response = await _mediator.Send(request);
            return response;
        }
    }
}
