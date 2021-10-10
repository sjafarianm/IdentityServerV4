using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAPI.Models
{
    public class ClientApplications
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientIdentifier { get; set; }
        public string AllowedGrantType { get; set; }
        /*
          public const string Implicit = "implicit";
        public const string Hybrid = "hybrid";
        public const string AuthorizationCode = "authorization_code";
        public const string ClientCredentials = "client_credentials";
        public const string ResourceOwnerPassword = "password";
        public const string DeviceFlow = "urn:ietf:params:oauth:grant-type:device_code";
             
             */
        public string ClientSecret { get; set; }
        public List<ClientScopes> AllowedScopes { get; set; }
        public ClientApplications()
        {
            AllowedScopes = new List<ClientScopes>();
        }
    }
}
