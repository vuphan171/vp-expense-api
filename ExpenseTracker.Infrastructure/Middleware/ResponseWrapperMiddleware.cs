using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Infrastructure.Middleware;

public class CustomResponseMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBody = context.Response.Body;

        try
        {
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await next(context);


            memoryStream.Position = 0;

            var statusCode = context.Response.StatusCode;
            var contentType = context.Response.ContentType;

            var isJson = contentType?.Contains("application/json",
                StringComparison.OrdinalIgnoreCase) == true;
            
            if (statusCode is 301 or 302 or 303 or 307 or 308 || !isJson && statusCode is not 401 and not 403)
            {
                context.Response.Body = originalBody;
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBody);
                return;
            }


            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            context.Response.Body = originalBody;
            context.Response.ContentType = "application/json";

            object? data = null;

            if (!string.IsNullOrWhiteSpace(responseBody))
            {
                try
                {
                    data = JsonSerializer.Deserialize<object>(responseBody);
                }
                catch
                {
                    data = responseBody;
                }
            }

            var wrappedResponse = new
            {
                StatusCode = statusCode,
                Success = statusCode is >= 200 and < 300,
                Data = data
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(wrappedResponse));
        }
        catch (Exception ex)
        {
            context.Response.Body = originalBody;
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                StatusCode = 500,
                Success = false,
                Error = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}

public static class CustomResponseMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomResponse(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomResponseMiddleware>();
    }
}