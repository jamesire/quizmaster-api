using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuizMaster.Api.Registries;
using QuizMaster.Domain.Clients;
using QuizMaster.Domain.Handlers;
using QuizMaster.Domain.QuizQuestionClient;
using QuizMaster.Storage.ActiveQuizzesTable;
using StructureMap;

namespace QuizMasterApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddSingleton(typeof(IActiveQuizzesTable), typeof(ActiveQuizzesTable));

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();

            services.AddMediatR(typeof(GetRandomQuizQuestionHandler).GetTypeInfo().Assembly);

            services.AddHttpClient<IOpenTdbClient, OpenTdbClient>((provider, client) =>
            {
                client.BaseAddress = new System.Uri(Configuration["Urls:OpenTdbUrl"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c => {
                //c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                c.WithOrigins(Configuration["Urls:ClientUrl"]).AllowAnyMethod().AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
