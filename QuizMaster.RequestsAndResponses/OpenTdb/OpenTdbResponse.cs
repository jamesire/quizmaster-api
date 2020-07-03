using QuizMaster.RequestsAndResponses.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QuizMaster.RequestsAndResponses.OpenTdb
{
    public class OpenTdbResponse
    {
        public IList<QuizQuestionDto> QuizQuestions { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; } 
    }
}
