using MediatR;
using OctoBridge.Domain.Common;

namespace OctoBridge.Application.CQRS.Abstractions;

/// <summary>
/// Base class for CQRS handlers. Enforces validation and ensures all handlers return ApiResponse<TResponse>.
/// </summary>
public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, ApiResponse<TResponse>> where TRequest : IRequest<ApiResponse<TResponse>>
{

    public async Task<ApiResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken = default)
    {
        return await ProcessAsync(request, cancellationToken);
    }

    /// <summary>
    /// Implement this to return ApiResponse<TResponse> directly.
    /// </summary>
    protected abstract Task<ApiResponse<TResponse>> ProcessAsync(TRequest request, CancellationToken cancellationToken);
}
