using EmiSoft.CleanArchitecture.Application.Interfaces;
using EmiSoft.CleanArchitecture.Application.Interfaces.Email;
using EmiSoft.CleanArchitecture.Application.Models.Config;
using EmiSoft.CleanArchitecture.Infrastructure.Data;
using EmiSoft.CleanArchitecture.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmiSoft.CleanArchitecture.Infrastructure;

public static class ConfigureServices
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));

        string connectionString = configuration.GetConnectionString("SqlServer");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString)
        );
        services.AddSingleton<IJwtService, JwtService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(EfUnitOfWork));

        JwtTokenConfiguration(services, configuration);

        #region Repositories


        #endregion

        return services;
    }

    private static void JwtTokenConfiguration(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtTokenConfig>(configuration.GetSection("JwtTokenConfig"));

        var jwtTokenConfig = new JwtTokenConfig();

        configuration.GetSection("JwtTokenConfig").Bind(jwtTokenConfig);

        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.IssuerSigningKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = jwtTokenConfig.ValidateIssuerSigningKey,
                      IssuerSigningKey = signingKey,
                      ValidateIssuer = jwtTokenConfig.ValidateIssuer,
                      ValidIssuer = jwtTokenConfig.ValidIssuer,
                      ValidateAudience = jwtTokenConfig.ValidateAudience,
                      ValidAudience = jwtTokenConfig.ValidAudience,
                      ValidateLifetime = jwtTokenConfig.ValidateLifetime,
                      ClockSkew = TimeSpan.Zero,
                      RequireExpirationTime = false,
                  };
              });

    }
}
