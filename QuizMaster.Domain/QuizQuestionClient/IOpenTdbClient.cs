using QuizMaster.Domain.Models;
using QuizMaster.RequestsAndResponses.OpenTdb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizMaster.Domain.QuizQuestionClient
{
    public interface IOpenTdbClient
    {
        public Task<OpenTdbResponse> GetQuestions(OpenTdbRequest request);
        public Task<OpenTdbResponse> GetRandomQuestion();
    }
}
