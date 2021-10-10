using IdentityServer.Infrastructure.Logging;
using IdentityServerAPI.DataAccess.User;
using IdentityServerAPI.DataAccess.Users;
using Microsoft.Extensions.DependencyInjection;


namespace IdentityServerAPI.Bootstrap
{
    public static class SingletonExtension
    {
        public static void AddSingletonService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();
            serviceCollection.AddSingleton<ILogger, NLogLogger>();  
        }

    }
}




