using EmiSoft.CleanArchitecture.SharedKernel.Utility;
using Microsoft.AspNetCore.Localization;
using System.Text.RegularExpressions;

namespace EmiSoft.CleanArchitecture.Web.DependencyInjection.Localization;

public class UrlRequestCultureProvider : RequestCultureProvider
{
    private static readonly Regex _localePattern = new(@"^[a-z]{2}(-[a-z]{2,4})?$",
                                                        RegexOptions.IgnoreCase);
    public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        var url = httpContext.Request.Path;

        // Right now it's not possible to use httpContext.GetRouteData()
        // since it uses IRoutingFeature placed in httpContext.Features when
        // Routing Middleware registers. It's not set when the Localization Middleware
        // is called, so this example simply assumes the locale will always 
        // be located in the second segment of a path, like in /api/en-US/products
        var parts = httpContext.Request.Path.Value.Split('/');
        if (parts.Length < 3)
        {
            return Task.FromResult(new ProviderCultureResult(Language.Azerbaijan.Culture));
        }

        if (!_localePattern.IsMatch(parts[2]))
        {
            return Task.FromResult(new ProviderCultureResult(Language.Azerbaijan.Culture));
        }

        var culture = parts[2];
        return Task.FromResult(new ProviderCultureResult(culture));
    }
}
