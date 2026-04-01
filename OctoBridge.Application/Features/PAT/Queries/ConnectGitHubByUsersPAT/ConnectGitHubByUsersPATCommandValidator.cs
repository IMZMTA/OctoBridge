using FluentValidation;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Domain.Constants.App;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByUsersPAT;

public class ConnectGitHubByUsersPATCommandValidator : BaseValidator<ConnectGitHubByUsersPATRequestDto>
{
    public ConnectGitHubByUsersPATCommandValidator()
    {

        RuleFor(x => x.PersonalAccessToken)
                .NotEmpty()
                .WithMessage(ValidationMessages.PATRequired)
                .NotNull()
                .MaximumLength(AppLimits.MaxTokenLength)
                .WithMessage(string.Format(ValidationMessages.TooLong, ErrorFields.PAT, AppLimits.MaxTokenLength))
                .Must(token => token.StartsWith("ghp_") || token.StartsWith("github_pat_"))
                .WithMessage(ValidationMessages.PATInvalidFormat);

    }
}
