using EmiSoft.CleanArchitecture.Application;
using EmiSoft.CleanArchitecture.Application.DependencyInjection.Localization;
using EmiSoft.CleanArchitecture.Application.Filters;
using EmiSoft.CleanArchitecture.Infrastructure;
using EmiSoft.CleanArchitecture.SharedKernel.Resources;
using EmiSoft.CleanArchitecture.Web.DependencyInjection.Swagger;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Serialization;

namespace EmiSoft.CleanArchitecture.Web.DependencyInjection.Api;

public static class ApiProjectInjection
{
    public static IServiceCollection AddApiProjectInjection(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        services
            .AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalValidationFilter));
                options.Filters.Add(typeof(GlobalApiExceptionFilter));
            })
            .AddFluentValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            })
            .AddDataAnnotationsLocalization(options =>
             {
                 options.DataAnnotationLocalizerProvider = (type, factory) =>
                 factory.Create(typeof(SharedResources));
             })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

        services.AddSwaggerDocumentation();

        services.AddCors();

        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

        services.AddHttpContextAccessor();

        services.AddHttpClient();

        services.AddOptions();

        services.AddLocalizationInjection();

        services.AddInfrastructure(configuration);

        services.AddApplication(configuration);

        return services;
    }
}
