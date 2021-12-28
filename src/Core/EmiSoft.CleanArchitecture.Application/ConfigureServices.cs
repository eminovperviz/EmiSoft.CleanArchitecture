using EmiSoft.CleanArchitecture.Application.Interfaces;
using EmiSoft.CleanArchitecture.Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace EmiSoft.CleanArchitecture.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped(typeof(IEntityService<>), typeof(BaseEntityService<>));

        #region Services



        #endregion


        return services;
    }
}
