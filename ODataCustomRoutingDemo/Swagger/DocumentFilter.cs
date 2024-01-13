using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using static System.Text.RegularExpressions.Regex;

namespace ODataCustomRoutingDemo.Swagger;

public class DocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        context.ApiDescriptions
            .Where(apiDescription => ((ControllerActionDescriptor)apiDescription.ActionDescriptor).ControllerName == "Metadata")
            .ToList()
            .ForEach(apiDescription => swaggerDoc.Paths.Remove($"/{apiDescription.RelativePath}"));

        swaggerDoc.Paths
            .Where(path => path.Key.Contains("$count"))
            .ToList()
            .ForEach(action =>
            {
                swaggerDoc.Paths.Remove(action.Key);
            });

        swaggerDoc.Paths
            .Where(path => IsMatch(path.Key, @"(.*)({key})"))
            .ToList()
            .ForEach(action =>
            {
                swaggerDoc.Paths.Remove(action.Key);
            });
    }
}
