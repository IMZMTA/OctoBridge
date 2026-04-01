using System.Net.Mime;
using FluentValidation;
using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Infrastructure.Exceptions;
using OctoBridge.Domain.Constants;

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
        SetNoCacheHeaders(context);

        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, ErrorMessages.ValidationFailed);

            var errors = ex.Errors.Select(e => new ApiError
            {
                Field = string.IsNullOrWhiteSpace(e.PropertyName) ? Messages.General : e.PropertyName,
                Message = e.ErrorMessage,
                Code = ErrorCode.ValidationFailed.ToString(),
                Source = ErrorSource.Internal.ToString()
            }).ToList();

            await WriteResponse(context, StatusCodes.Status400BadRequest, ErrorMessages.ValidationFailed, errors);
        }
        catch (ExternalApiException ex)
        {
            _logger.LogError(ex, "External API Error");

            await WriteResponse(
                context,
                (int)ex.StatusCode,
                ErrorMessages.ExternalService,
                ex.Errors
            );
        }
        catch (AuthenticationException ex)
        {
            _logger.LogWarning(ex, ErrorMessages.AuthenticationFailed);

            await WriteResponse(context,
                StatusCodes.Status401Unauthorized,
                ErrorMessages.AuthenticationFailed,
                BuildSingleError(ex.Field, ex.Message, ErrorCode.AuthenticationFailed));
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, ErrorMessages.UnauthorizedAccess);

            await WriteResponse(context,
                StatusCodes.Status403Forbidden,
                ErrorMessages.UnauthorizedAccess,
                BuildSingleError(ErrorCode.AuthorizationFailed.ToString(), ErrorMessages.UserOrSessionExpired, ErrorCode.AuthorizationFailed));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorMessages.Unexpected);

            await WriteResponse(context,
                StatusCodes.Status500InternalServerError,
                ErrorMessages.Unexpected,
                BuildSingleError(Messages.General, ex.Message, ErrorCode.InternalServerError));
        }
    }

    private static void SetNoCacheHeaders(HttpContext context)
    {
        context.Response.Headers.CacheControl = HeaderConstants.CacheControl;
        context.Response.Headers.Pragma = HeaderConstants.Pragma;
        context.Response.Headers.Expires = HeaderConstants.Expires;
    }

    private static List<ApiError> BuildSingleError(string field, string message, ErrorCode code)
    {
        return new List<ApiError>
        {
            new()
            {
                Field = field,
                Message = message,
                Code = code.ToString(),
                Source = ErrorSource.Internal.ToString()
            }
        };
    }

    private static async Task WriteResponse(HttpContext context, int statusCode, string message, List<ApiError> errors)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var response = new ApiResponse<string>
        {
            Success = false,
            Messages = new List<string> { message },
            Errors = errors
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}