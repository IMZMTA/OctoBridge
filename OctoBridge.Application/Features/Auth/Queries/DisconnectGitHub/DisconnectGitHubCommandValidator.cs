using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Auth.Queries.DisconnectGitHub;

public class DisconnectGitHubCommandValidator : BaseValidator<DisconnectGitHubRequestDto>
{
    public DisconnectGitHubCommandValidator()
    {

    }
}
