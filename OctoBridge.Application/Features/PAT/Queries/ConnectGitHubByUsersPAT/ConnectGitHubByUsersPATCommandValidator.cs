using FluentValidation;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByUsersPAT;

public class ConnectGitHubByUsersPATCommandValidator : BaseValidator<ConnectGitHubByUsersPATRequestDto>
{
    public ConnectGitHubByUsersPATCommandValidator()
    {

        RuleFor(x => x.PersonalAccessToken)
                .NotEmpty()
                .WithMessage(Messages.PATRequired)
                .NotNull()
                .MaximumLength(AppConstants.MaxTokenLength)
                .WithMessage(string.Format(Messages.PATTooLong,AppConstants.MaxTokenLength))
                .Must(token => token.StartsWith("ghp_") || token.StartsWith("github_pat_"))
                .WithMessage(Messages.PATInvalidFormat);

    }
}
