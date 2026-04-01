using FluentValidation;
using OctoBridge.Domain.Constants.App;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByServerPAT;

public class ConnectGitHubByServerPATCommandValidator : BaseValidator<ConnectGitHubByServerPATRequestDto>
{
    public ConnectGitHubByServerPATCommandValidator()
    {
        RuleFor(x => x.ConnectorName)
            .MaximumLength(AppLimits.MaxLabelNameLength)
            .WithMessage(string.Format(ValidationMessages.ConnectorNameTooLong, AppLimits.MaxLabelNameLength));
    }
}
