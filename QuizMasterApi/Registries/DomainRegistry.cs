using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuizMaster.Domain.Clients;
using QuizMaster.Domain.QuizQuestionClient;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuizMaster.Api.Registries
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            For<IOpenTdbClient>().Use("default", p =>
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(configuration["Urls:OpenTdbUrl"])
                };

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );

                return new OpenTdbClient(client, loggerFactory.CreateLogger<OpenTdbClient>());
            });
            
            For<IMediator>().Use<Mediator>();
        }
    }
}
