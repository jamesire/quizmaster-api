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
using System.Diagnostics;
using System.Linq;
using System.Web;

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
                    response.ReasonPhrase = httpResponse.ReasonPhrase;
                    response.StatusCode = httpResponse.StatusCode;

                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var quizQuestionsAsJsonObject = PrepareTextForJsonObjectification(responseContent);
                    response.QuizQuestions = MapJsonToQuizQuestions(quizQuestionsAsJsonObject);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception: " + ex.ToString());
            }
            
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

        private JToken PrepareTextForJsonObjectification(string jsonToBeModified)
        { 
            var jObject = JObject.Parse(jsonToBeModified);
            //var jsonToBeReturned = JsonConvert.SerializeObject(jObject.ToString());
            var jsonToBeReturned = jObject["results"];
            return jsonToBeReturned;
        }

        private List<QuizQuestionDto> MapJsonToQuizQuestions(JToken jArray)
        {
            List<QuizQuestionDto> questionDtoList = new List<QuizQuestionDto>();
            foreach (var token in jArray)
            {
                var questionDto = new QuizQuestionDto();
                
                var categoryString = token.Value<string>("category");
                var typeString = token.Value<string>("type");
                var difficultyString = token.Value<string>("difficulty");
                var wrongAnswers = (token["incorrect_answers"] as JArray).ToObject<List<string>>();

                wrongAnswers.ForEach(str => HttpUtility.HtmlDecode(str));

                questionDto.Question = HttpUtility.HtmlDecode(token.Value<string>("question"));
                questionDto.CorrectAnswer = HttpUtility.HtmlDecode(token.Value<string>("correct_answer"));
                questionDto.IncorrectAnswers = wrongAnswers;

                questionDto.Category = EnumExtensionMethods.GetEnumValueFromDescription<Categories>(categoryString);
                questionDto.Type = EnumExtensionMethods.GetEnumValueFromDescription<QuestionTypes>(typeString);
                questionDto.Difficulty = EnumExtensionMethods.GetEnumValueFromDescription<Difficulties>(difficultyString);

                questionDtoList.Add(questionDto);
            }

            return questionDtoList;
        }
    }
}
