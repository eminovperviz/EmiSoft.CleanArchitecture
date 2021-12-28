﻿using EmiSoft.CleanArchitecture.Application.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EmiSoft.CleanArchitecture.Application.DependencyInjection.Swagger;

public static class SwaggerInjection
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string assemblyName)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmiSoft.CleanArchitecture Services", Version = "v1", Description = "API Services" });
            c.DocumentFilter<SwaggerDocumentFilter>();
            Dictionary<string, IEnumerable<string>> security = new()
            {
                { "Bearer", Array.Empty<string>() },
            };

            c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
            {
              new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  },
                  Scheme = "Bearer",
                  Name = "Authorization",
                  In = ParameterLocation.Header,
                },
                new List<string>()
              }
            });

            IncludeXmlComments(assemblyName, c);

            c.CustomSchemaIds(x => x.FullName);
        });

        return services;
    }

    private static void IncludeXmlComments(string assemblyName, SwaggerGenOptions c)
    {
        string xmlFile = $"{assemblyName}.xml";

        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        c.IncludeXmlComments(xmlPath);
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string apiName = "Api v1")
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
            c.DefaultModelRendering(ModelRendering.Example);
            c.DefaultModelExpandDepth(1);
        });

        return app;
    }
}
