using EmiSoft.CleanArchitecture.Application.DependencyInjection.Api;
using EmiSoft.CleanArchitecture.Application.DependencyInjection.Swagger;
using EmiSoft.CleanArchitecture.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddSwaggerDocumentation(Assembly.GetExecutingAssembly().GetName().Name);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiProjectInjection(builder.Configuration);
builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizeOptions.Value);

app.UseRouting();

app.UseSwaggerDocumentation();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.Run();
