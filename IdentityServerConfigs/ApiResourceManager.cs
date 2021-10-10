using Dapper;
using IdentityServer4.Models;
using IdentityServerAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAPI.IdentityServerConfigs
{
    public class ApiResourceManager
    {
        private IConfiguration _configuration;
        public ApiResourceManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<ApiResource> Apis()
        {
            var connectionstring = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {

                var apiResources = conn.Query<ApiResources, ClientScopes, ApiResources>("ApiResource_GetAll",
                    (apiResource, scope) =>
                    {
                        apiResource.ApiResourceScopes.Add(scope);
                        return apiResource;
                    }, splitOn: "ScopeId");

                var result = apiResources.GroupBy(c => c.Id).Select(g =>
                {
                    var groupedClient = g.First();
                    groupedClient.ApiResourceScopes = g.Select(p => p.ApiResourceScopes.Single()).ToList();
                    return groupedClient;
                });

                var finalResult = result.Select(x => new ApiResource(x.Name, x.Description)
                {
                    ApiSecrets = { new Secret(x.SecretKey.Sha256()) },

                    Scopes = x.ApiResourceScopes.Select(x => x.Scope).ToList()
                }
                ).ToList();
                return finalResult;
            }

        }
    }
}
