using Microsoft.Extensions.Logging;
using QuizMaster.Domain.Models;
using QuizMaster.Domain.QuizQuestionClient;
using QuizMaster.RequestsAndResponses.Enums;
using QuizMaster.RequestsAndResponses.OpenTdb;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizMaster.RequestsAndResponses.Models;
using Newtonsoft.Json.Linq;

namespace QuizMaster.Domain.Clients
{
    public class OpenTdbClient : IOpenTdbClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<OpenTdbClient> _logger;
        private string _queryString = "";

        public OpenTdbClient(HttpClient client, ILogger<OpenTdbClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<OpenTdbResponse> GetQuestions(OpenTdbRequest request)
        {
            var response = new OpenTdbResponse();
            _queryString = "?amount=" + request.NumberOfQuestionsToGenerate;

            AssembleQueryString(request);

            try
            {
                var httpResponse = await _client.GetAsync(_queryString);
                if(httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var quizQuestionsAsJson = PrepareTextForJsonObjectification(responseContent);
                    response.QuizQuestions = JsonConvert.DeserializeObject<List<QuizQuestionDto>>(quizQuestionsAsJson);
                }
                else
                {
                    response.ReasonPhrase = httpResponse.ReasonPhrase;
                    response.StatusCode = httpResponse.StatusCode;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
            }
            
            return response;
        }

        public Task<OpenTdbResponse> GetRandomQuestion()
        {
            var request = new OpenTdbRequest();
            var response = GetQuestions(request);

            return response;
        }

        private void AssembleQueryString(OpenTdbRequest request)
        {
            if (request.Category != Categories.AnyCategory)
            {
                _queryString += "&category=" + (int)request.Category;
            }
            if (request.Difficulty != Difficulties.Any)
            {
                _queryString += "&difficulty=" + request.Difficulty.ToString();
            }
            if (request.Type != QuestionTypes.Any)
            {
                _queryString += "&type=" + request.Type.ToString();
            }
        }

        private string PrepareTextForJsonObjectification(string jsonToBeModified)
        { 
            var jObject = JObject.Parse(jsonToBeModified);
            jObject.Remove("response_code");
            var jsonToBeReturned = JsonConvert.SerializeObject(jObject.ToString());
            return jsonToBeReturned;
        }
    }
}
