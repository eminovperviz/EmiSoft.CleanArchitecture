using EmiSoft.CleanArchitecture.Application.Interfaces;
using EmiSoft.CleanArchitecture.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmiSoft.CleanArchitecture.Infrastructure.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServer");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(EfUnitOfWork));

        #region Repositories


        #endregion

        return services;
    }
}
