using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _environment = env;
        }



        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransientService();
            services.AddSingletonService();
            services.AddScopedService();
            services.AddHttpContextAccessor();
            services.AddPerformance();
            services.AddControllers();

            if (_environment.IsDevelopment())
            {
                services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiScopes(new ScopesManager(Configuration).Scopes())
                    .AddInMemoryApiResources(new ApiResourceManager(Configuration).Apis())
                    .AddInMemoryClients(new ClientsManager(Configuration).Clients())
                    .AddProfileService<ProfileService>();
            }
            else
            {
                var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, Configuration.GetSection("identityServerCertificateName").Value),
                    Configuration.GetSection("identityServerCertificateSecretKey").Value);
                services.AddIdentityServer()
                      .AddSigningCredential(cert)
                      .AddInMemoryApiScopes(new ScopesManager(Configuration).Scopes())
                      .AddInMemoryApiResources(new ApiResourceManager(Configuration).Apis())
                      .AddInMemoryClients(new ClientsManager(Configuration).Clients())
                      .AddProfileService<ProfileService>();

            }


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
