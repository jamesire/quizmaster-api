using QuizMaster.RequestsAndResponses.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QuizMaster.RequestsAndResponses.OpenTdb
{
    public class OpenTdbResponse : Response
    {
        public IList<QuizQuestionDto> QuizQuestions { get; set; }
    }
}
