using Dapper;
using IdentityServer.Infrastructure.Logging;
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
    public class ScopesManager
    {
        private IConfiguration _configuration;
        private ILogger _logger;
        public ScopesManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<ApiScope> Scopes()
        {
            var connectionstring = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                var allScopes = conn.Query<Scopes>("Scopes_GetAll");
                var finalResult = allScopes.Select(x => new ApiScope(x.Scope)).ToList();
                return finalResult;
            }

        }
    }
}
