using OctoBridge.Domain.Constants;

namespace OctoBridge.Infrastructure.Exceptions;

public class AuthenticationException : Exception
{
    public string Field { get; }

    public AuthenticationException(string message, string field = ErrorFields.Authentication) : base(message)
    {
        Field = field;
    }
}
