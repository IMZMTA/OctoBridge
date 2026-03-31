namespace OctoBridge.Application.Features.Auth.Queries.LoginProviderUrl;

public class LoginProviderUrlResponseDto
{
    public string RedirectUrl { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
}
