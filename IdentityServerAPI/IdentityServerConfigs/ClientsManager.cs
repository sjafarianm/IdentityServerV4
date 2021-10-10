using Dapper;
using IdentityServer.Infrastructure.Logging;
using IdentityServer4.Models;
using IdentityServerAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAPI.IdentityServerConfigs
{
    public class ClientsManager
    {
        private IConfiguration _configuration;
        public ClientsManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Client> Clients()
        {
            var connectionstring = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {

                var allClients = conn.Query<ClientApplications, ClientScopes, ClientApplications>("ClientApplications_GetAll",
                    (client, scope) =>
                    {
                        client.AllowedScopes.Add(scope);
                        return client;
                    }, splitOn: "ScopeId");

                var result = allClients.GroupBy(c => c.Id).Select(g =>
                {
                    var groupedClient = g.First();
                    groupedClient.AllowedScopes = g.Select(p => p.AllowedScopes.Single()).ToList();
                    return groupedClient;
                });

                var finalResult = result.Select(x => new Client
                {
                    ClientName = x.ClientName,
                    ClientId = x.ClientIdentifier,
                    AllowedGrantTypes = new List<string> { x.AllowedGrantType },
                    ClientSecrets = { new Secret(x.ClientSecret.Sha256()) },
                    AllowedScopes = x.AllowedScopes.Select(y => y.Scope).ToList()
                }).ToList();
                return finalResult;
            }

        }
    }
}
