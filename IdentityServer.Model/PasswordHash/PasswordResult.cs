namespace IdentityServer.Model.PasswordHash
{
    public class PasswordResult
    {
        public byte[] Signature { get; set; }

        public byte[] Salt { get; set; }
    }
}