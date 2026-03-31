using OctoBridge.Application.CQRS.Abstractions;

namespace OctoBridge.Application.Features.Auth.Queries.Logout;

public class LogoutCommandValidator : BaseValidator<LogoutRequestDto>
{
    public LogoutCommandValidator()
    {

    }
}
