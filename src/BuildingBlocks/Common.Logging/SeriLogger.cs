using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Config =>
         (context, configuration) =>
         {
             var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

             configuration.Enrich.FromLogContext()
                              .Enrich.WithMachineName()
                              .WriteTo.Console()
                              .WriteTo.Elasticsearch(
                                new ElasticsearchSinkOptions(new Uri(elasticUri))
                                {
                                    IndexFormat = $"applogs-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                                    AutoRegisterTemplate = true,
                                    NumberOfShards = 2,
                                    NumberOfReplicas = 1,
                                }
                             )
                             .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                             .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                             .ReadFrom.Configuration(context.Configuration);

         };
    }
}
