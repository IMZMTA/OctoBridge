using FluentValidation;

namespace OctoBridge.Application.CQRS.Abstractions;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected BaseValidator()
    {
    }
}
