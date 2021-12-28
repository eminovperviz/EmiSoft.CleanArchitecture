using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmiSoft.CleanArchitecture.Application.Filters;

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathsToRemove = swaggerDoc.Paths
               .Where(pathItem => pathItem.Key.Contains("{culture}/"))
               .ToList();

        foreach (var item in pathsToRemove)
        {
            swaggerDoc.Paths.Remove(item.Key);
        }
    }
}
