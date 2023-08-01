using System.Net.Mime;
using System.Security.Authentication;
using FluentValidation;
using Persistence.Exceptions;
using WebApi.ViewModel;

namespace WebApi.Middleware;

public class ErrorHandlerMiddleware
{
    private const string InternalServerErrorText = "Internal Server Error.";
    private const string EntityNotFoundErrorText = "Entity not found.";
    private const string ValidationExceptionText = "Entered data is incorrect";
    private const string AuthenticationExceptionText = "Forbidden";

    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, string.Empty);

        var response = context.Response;

        var (statusCode, error) = GetStatusCodeAndError(exception);
        response.ContentType = MediaTypeNames.Application.Json;
        response.StatusCode = statusCode;

        var errorDetailsViewModel = new ErrorDetailsViewModel
        {
            StatusCode = statusCode,
            Error = error,
            Message = exception.GetBaseException().Message,
            StackTrace = exception.GetBaseException().StackTrace!
        };

        return context.Response.WriteAsJsonAsync(errorDetailsViewModel);
    }

    private (int StatusCode, string Error) GetStatusCodeAndError(Exception exception)
    {
        int statusCode;
        string error;

        switch (exception)
        {
            case EntityNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                error = EntityNotFoundErrorText;
                break;
            case ArgumentException:
            case AuthenticationException:
            case ValidationException:
                statusCode = StatusCodes.Status400BadRequest;
                error = ValidationExceptionText;
                break;
            case InvalidOperationException:
                statusCode = StatusCodes.Status403Forbidden;
                error = AuthenticationExceptionText;
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                error = InternalServerErrorText;
                break;
        }

        return (statusCode, error);
    }
}