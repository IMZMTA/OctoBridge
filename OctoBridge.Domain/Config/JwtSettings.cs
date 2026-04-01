namespace OctoBridge.Domain.Config;
public class JwtSettings
{
    public string Key { get; init; } = string.Empty;

    public string Issuer { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;

    public int ExpirationMinutes { get; init; } = 120;
    public int ExpirationHours { get; init; } = 2;
    public int ExpirationDays { get; init; } = 7;
}