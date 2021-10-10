using IdentityServer.Model.PasswordHash;

namespace IdentityServer.Infrastructure.Cryptography
{
    public interface IPasswordHash
    {
        PasswordResult Encrypt(string password);
        bool VerifySignature(string password, byte[] signature, byte[] salt);
    }
}