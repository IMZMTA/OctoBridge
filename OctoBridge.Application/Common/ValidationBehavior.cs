using MediatR;
using FluentValidation;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Application.Common;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = (await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))))
                .SelectMany(r => r.Errors)
                .Where(e => e != null)
                .ToList();

            if (failures.Count != 0)
            {
                var responseType = typeof(TResponse);

                if (responseType.IsGenericType &&
                    responseType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    var errorList = failures
                        .Select(f => new ApiError
                        {
                            Field = string.IsNullOrWhiteSpace(f.PropertyName) ? Messages.General : f.PropertyName,
                            Message = f.ErrorMessage
                        })
                        .ToList();

                    var errorInstance = Activator.CreateInstance(responseType)!;
                    responseType.GetProperty("Success")?.SetValue(errorInstance, false);
                    responseType.GetProperty("Messages")?.SetValue(errorInstance, new List<string> { "Validation Failed" });
                    responseType.GetProperty("Errors")?.SetValue(errorInstance, errorList);

                    return (TResponse)errorInstance;
                }

                throw new ValidationException(failures);
            }

        }

        return await next();
    }
}
