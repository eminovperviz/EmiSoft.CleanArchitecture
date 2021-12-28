using EmiSoft.CleanArchitecture.SharedKernel.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace EmiSoft.CleanArchitecture.Application.DependencyInjection.Localization;

public static class LocalizationInjection
{
    public static IServiceCollection AddLocalizationInjection(this IServiceCollection services)
    {
        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo("az-AZ"), new CultureInfo("en-US"), new CultureInfo("ru-RU") };
            options.DefaultRequestCulture = new RequestCulture("az", "az");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider
            {
                Options = options
            });

        });
        services.AddControllers()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(SharedResources));
            });

        return services;
    }
}
