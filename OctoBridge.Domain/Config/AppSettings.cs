using OctoBridge.Domain.Constants;

namespace OctoBridge.Domain.Config;

public class AppSettings
{
    public bool SwaggerEnabled { get; init; }
    public string[] AllowedOrigins { get; init; } = Array.Empty<string>();
    public JwtSettings Jwt { get; init; } = new();
    public EncryptionSettings Encryption { get; init; } = new();
    public ProviderSettings Providers { get; init; } = new();
}