using FluentValidation;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Auth.Queries.HandleOAuthCallback;

public class HandleOAuthCallbackCommandValidator : BaseValidator<HandleOAuthCallbackRequestDto>
{
    public HandleOAuthCallbackCommandValidator()
    {
        RuleFor(x => x.Provider)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidProvider);
    }
}
