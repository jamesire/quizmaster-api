using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QuizMaster.RequestsAndResponses
{
    public abstract class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
    }
}
