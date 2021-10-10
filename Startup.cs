using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using IdentityServerAPI.Bootstrap;
using IdentityServerAPI.IdentityServerConfigs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServerAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransientService();
            services.AddSingletonService();
            services.AddScopedService();
            services.AddHttpContextAccessor();
            services.AddPerformance();
            services.AddControllers();

            services.AddIdentityServer()
             .AddDeveloperSigningCredential()
             .AddInMemoryApiScopes(new ScopesManager(Configuration).Scopes())
             .AddInMemoryApiResources(new ApiResourceManager(Configuration).Apis())
             .AddInMemoryClients(new ClientsManager(Configuration).Clients())
             .AddProfileService<ProfileService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIdentityServer();
            app.UsePerformance();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
