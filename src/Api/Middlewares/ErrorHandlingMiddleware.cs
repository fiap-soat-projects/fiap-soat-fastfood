using Business.Entities;
using Business.Exceptions;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) when (ex
            is CustomerNotFoundException
            or MenuItemNotFoundException
            or OrderNotFoundException
            or DuplicatedItemException<Customer>
            or DuplicatedItemException<MenuItem>)
        {
            _logger.LogWarning(ex, "{Message}", ex.Message);

            await HandleResponseAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            await HandleResponseAsync(context, ex, HttpStatusCode.InternalServerError);
        }
    }

    private static async Task HandleResponseAsync(
        HttpContext context,
        Exception ex,
        HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = context.Response.StatusCode,
            error = ex.Message
#if DEBUG
            ,
            details = ex.StackTrace
#endif
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonSerializerOptions));
    }
}
