using System.Net.Mime;
using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.Common;
using OctoBridge.Infrastructure.Exceptions;

namespace OctoBridge.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.CacheControl = AppConstants.CacheControlValue;
        context.Response.Headers.Pragma = AppConstants.PragmaValue;
        context.Response.Headers.Expires = AppConstants.HeaderExpiresValue;

        try
        {
            await _next(context);
        }
        catch (ValidationException validationEx)
        {
            _logger.LogWarning(validationEx, Messages.ValidationFailed);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var errors = validationEx.Errors
                .Select(e => new ApiError
                {
                    Field = string.IsNullOrWhiteSpace(e.PropertyName) ? Messages.General : e.PropertyName,
                    Message = e.ErrorMessage
                })
                .ToList();

            var response = new ApiResponse<string>
            {
                Success = false,
                Messages = new List<string> { Messages.ValidationFailed },
                Errors = errors
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (AuthenticationException authEx)
        {
            _logger.LogWarning(authEx, Messages.AuthenticationFailed);
            await WriteErrorResponse(context, StatusCodes.Status401Unauthorized, Messages.AuthenticationFailed, authEx.Field, authEx.Message);
        }
        catch (UnauthorizedAccessException unauthEx)
        {
            _logger.LogWarning(unauthEx, Messages.UnauthorizedAccess);
            await WriteErrorResponse(context, StatusCodes.Status401Unauthorized, Messages.SessionExpired, AppConstants.Session, Messages.UserOrSessionExpired);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, Messages.UnexpectedError);
            await WriteErrorResponse(context, StatusCodes.Status500InternalServerError, Messages.UnexpectedError, Messages.General, ex.Message);
        }
    }
    private static async Task WriteErrorResponse(HttpContext context, int statusCode, string mainMessage, string field, string detailMessage)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var response = new ApiResponse<string>
        {
            Success = false,
            Messages = new List<string> { mainMessage },
            Errors = new List<ApiError>
            {
                new() { Field = field, Message = detailMessage }
            }
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
