using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;



namespace IdentityServerAPI.Bootstrap
{

    public static class TransientExtension
    {
        public static void AddTransientService(this IServiceCollection services)
        {
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
        }
    }
}




