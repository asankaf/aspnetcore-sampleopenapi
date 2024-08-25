using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.Mime;

namespace AspNetCore.SampleOpenApi.Transformers;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by the OpenApi library")]
internal sealed class ProblemDetailsOperationTransformer : IOpenApiOperationTransformer
{
    private const string _problemDetailsMediaTypeName = "application/problem+json";

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {

        foreach (var responseType in context.Description.SupportedResponseTypes) {

            if (responseType.StatusCode >= 200 && responseType.StatusCode < 300) {
                continue;
            }

            var responseKey = responseType.StatusCode == 0
                ? "default"
                : responseType.StatusCode.ToString(CultureInfo.InvariantCulture);

            var response = operation.Responses[responseKey];

            // If response CLR type/schema is ProblemDetails, and response has
            // "application/json" content type but no "application/problem+json"
            // content type, then change "application/json" to
            // "application/problem+json".
            if (
                responseType.ModelMetadata?.ModelType == typeof(ProblemDetails)
                &&
                !response.Content.ContainsKey(_problemDetailsMediaTypeName)
                &&
                response.Content.TryGetValue(MediaTypeNames.Application.Json, out var applicationJsonContent)
            ) {
                response.Content.Remove(MediaTypeNames.Application.Json);
                response.Content.Add(_problemDetailsMediaTypeName, applicationJsonContent);
            }
        }

        return Task.CompletedTask;
    }
}
