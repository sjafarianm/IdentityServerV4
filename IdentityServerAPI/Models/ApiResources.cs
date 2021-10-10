using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAPI.Models
{
    public class ApiResources
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SecretKey { get; set; }
        public List<ClientScopes> ApiResourceScopes { get; set; }
        public ApiResources()
        {
            ApiResourceScopes = new List<ClientScopes>(); 
        }
    }
}
