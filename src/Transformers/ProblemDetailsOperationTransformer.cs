using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace AspNetCore.SampleOpenApi.Transformers;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by the OpenApi library")]
internal sealed class ProblemDetailsOperationTransformer : IOpenApiOperationTransformer
{
    private const string _problemDetailsMediaTypeName = "application/problem+json";

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        foreach (var (status, response) in operation.Responses) {
            if (!IsErrorResponseStatus(status)) {
                continue;
            }
            if (
                !response.Content.ContainsKey(_problemDetailsMediaTypeName)
                &&
                response.Content.TryGetValue(MediaTypeNames.Application.Json, out var applicationJsonContent)
                &&
                IsProblemDetailsSchema(applicationJsonContent.Schema)
            ) {
                response.Content.Add(_problemDetailsMediaTypeName, applicationJsonContent);
                response.Content.Remove(MediaTypeNames.Application.Json);
            }
        }

        return Task.CompletedTask;
    }

    private static bool IsErrorResponseStatus(string status) {
        var result =
            status.Equals("default", StringComparison.OrdinalIgnoreCase)
            ||
            (int.TryParse(status, out var statusCode) && statusCode >= 400);

        return result;
    }

    private static bool IsProblemDetailsSchema(OpenApiSchema schema) {
        var stringType = "string";
        var intType = "integer";
        var result =
            schema.Type == "object" &&
            HasSchemaProperty(schema.Properties, "type", stringType) &&
            HasSchemaProperty(schema.Properties, "title", stringType) &&
            HasSchemaProperty(schema.Properties, "status", intType) &&
            HasSchemaProperty(schema.Properties, "detail", stringType) &&
            HasSchemaProperty(schema.Properties, "instance", stringType);

        return result;
    }

    private static bool HasSchemaProperty(IDictionary<string, OpenApiSchema> properties, string propName, string propType) {
        var result = properties.ContainsKey(propName) && properties[propName].Type == propType;
        return result;
    }
}
