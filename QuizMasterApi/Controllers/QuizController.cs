using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizMasterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }

        [HttpGet("random")]
        public IEnumerable<QuizController> GetRandomQuestion()
        {
            return null;
        }
    }
}
