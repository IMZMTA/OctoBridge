using FluentValidation;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Application.Features.Auth.Queries.HandleOAuthCallback;

public class HandleOAuthCallbackCommandValidator : BaseValidator<HandleOAuthCallbackRequestDto>
{
    public HandleOAuthCallbackCommandValidator()
    {
        RuleFor(x => x.Provider)
            .IsInEnum()
            .WithMessage(Messages.InvalidProvider);
    }
}
