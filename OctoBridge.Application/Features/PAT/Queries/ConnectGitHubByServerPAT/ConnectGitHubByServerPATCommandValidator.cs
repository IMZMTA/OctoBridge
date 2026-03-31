using FluentValidation;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByServerPAT;

public class ConnectGitHubByServerPATCommandValidator : BaseValidator<ConnectGitHubByServerPATRequestDto>
{
    public ConnectGitHubByServerPATCommandValidator()
    {
        RuleFor(x => x.ConnectorName)
            .MaximumLength(AppConstants.MaxLabelNameLength)
            .WithMessage(Messages.ConnectorNameTooLong);
    }
}
