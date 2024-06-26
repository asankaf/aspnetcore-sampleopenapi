using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

namespace Microsoft.AspNetCore.Builder;

internal static class WebApplicationExtensions
{
    public static WebApplication MapCustomOpenApi(this WebApplication app)
    {
        app.MapOpenApi("/openapi/{documentName}.json");

        return app;
    }

    public static WebApplication UseCustomHttps(this WebApplication app)
    {
        app.UseHttpsRedirection();

        return app;
    }

    public static WebApplication UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        return app;
    }

    public static WebApplication UseCustomAuthorization(this WebApplication app)
    {
        app.UseAuthorization();

        return app;
    }

    public static WebApplication MapCustomControllers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
