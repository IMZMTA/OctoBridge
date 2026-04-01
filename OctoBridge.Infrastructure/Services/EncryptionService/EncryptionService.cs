using System.Text;
using OctoBridge.Domain.Config;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace OctoBridge.Infrastructure.Services.Security;

public class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public EncryptionService(IOptions<AppSettings> options)
    {
        _key = Convert.FromBase64String(options.Value.Encryption.Key);
        _iv = Convert.FromBase64String(options.Value.Encryption.IV);
    }

    public string Encrypt(string plaintext)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        using var encryptor = aes.CreateEncryptor();
        var bytes = Encoding.UTF8.GetBytes(plaintext);
        var encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string ciphertext)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        using var decryptor = aes.CreateDecryptor();
        var bytes = Convert.FromBase64String(ciphertext);
        var decrypted = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        return Encoding.UTF8.GetString(decrypted);
    }
}


