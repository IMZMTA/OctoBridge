namespace OctoBridge.Domain.Config;
public class EncryptionSettings
{
    public const string SectionName = "AppSettings:Encryption";

    public string Key { get; init; } = default!;

    public string IV { get; init; } = default!;
}