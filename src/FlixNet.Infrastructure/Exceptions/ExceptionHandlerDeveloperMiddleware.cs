using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FlixNet.Infrastructure.Exceptions;

public class ExceptionHandlerDeveloperMiddleware(
    ILogger<ExceptionHandlerDeveloperMiddleware> logger,
    IExceptionToResponseDeveloperMapper exceptionToResponseDeveloperMapper) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var exceptionResponse = exceptionToResponseDeveloperMapper.Map(exception);
        context.Response.StatusCode = (int)exceptionResponse.StatusCode;
        context.Response.ContentType = "application/json";
        await JsonSerializer.SerializeAsync(context.Response.Body, exceptionResponse.Data,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
