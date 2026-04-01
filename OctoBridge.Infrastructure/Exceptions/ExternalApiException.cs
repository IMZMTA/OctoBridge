using System.Net;
using OctoBridge.Domain.Common;

namespace OctoBridge.Infrastructure.Exceptions;

public class ExternalApiException : Exception
{
    public List<ApiError> Errors { get; }
    public HttpStatusCode StatusCode { get; }

    public ExternalApiException(string message, List<ApiError> errors, HttpStatusCode statusCode) 
        : base(message)
    {
        Errors = errors;
        StatusCode = statusCode;
    }
}