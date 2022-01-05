using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Elasticsearch;

namespace EmiSoft.CleanArchitecture.Application.Extensions;

public class LoggerExtension
{
    public static Logger CreateLogger(IConfiguration configuration)
    {
        return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearchConfig:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"logs-{DateTime.UtcNow:yyyy-MM}"
                })
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .CreateLogger();
    }
}
