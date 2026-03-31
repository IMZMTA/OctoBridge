using FluentValidation;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Auth.Queries.LoginProviderUrl;

public class LoginProviderUrlCommandValidator : BaseValidator<LoginProviderUrlRequestDto>
{
    public LoginProviderUrlCommandValidator()
    {
        RuleFor(x => x.Provider)
            .IsInEnum()
            .WithMessage(Messages.InvalidProvider);
    }
}
