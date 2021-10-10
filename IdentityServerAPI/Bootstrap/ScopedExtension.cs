using IdentityServer.Infrastructure.Cryptography;
using IdentityServerAPI.DataAccess.User;
using IdentityServerAPI.DataAccess.Users;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerAPI.Bootstrap
{
    public static class ScopedExtension
    {

        public static void AddScopedService(this IServiceCollection serviceCollection)
        {
           
            serviceCollection.AddScoped<IPasswordHash, PasswordHash>();
        }
    }
}