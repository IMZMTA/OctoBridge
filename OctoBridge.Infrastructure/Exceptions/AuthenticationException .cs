using OctoBridge.Domain.Constants;

namespace OctoBridge.Infrastructure.Exceptions;

public class AuthenticationException : Exception
{
    public string Field { get; }

    public AuthenticationException(string message, string field = Messages.General) : base(message)
    {
        Field = field;
    }
}
