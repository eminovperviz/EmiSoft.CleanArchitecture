using EmiSoft.CleanArchitecture.Application.Extensions;
using EmiSoft.CleanArchitecture.Web.DependencyInjection.Api;
using EmiSoft.CleanArchitecture.Web.DependencyInjection.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddApiProjectInjection(builder.Configuration);
builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);

Log.Logger = LoggerExtension.CreateLogger(builder.Configuration);

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
