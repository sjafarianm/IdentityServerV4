using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = LoadConfig(args);
            var hostUrl = CreateHostUrl(args, config);
            CreateHostBuilder(args, hostUrl).Build().Run();
        }


        private static IConfigurationRoot LoadConfig(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddCommandLine(args)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            return config;
        }

        private static string[] CreateHostUrl(string[] args, IConfigurationRoot config)
        {
            var hostUrl = config.GetSection("hosturl").Value;
            if (string.IsNullOrEmpty(hostUrl))
                hostUrl = "http://0.0.0.0:8001";
            var split = hostUrl.Split(":");

            int port = 0;
            int.TryParse(split[2], out port);
            if (args.Length > 0)
            {
                var portArgument = args.FirstOrDefault(p => p.ToLower().StartsWith("-p"));
                if (portArgument != null)

                    if (int.TryParse(portArgument.Replace("-p", ""), out port))
                    {
                        hostUrl = $"http://0.0.0.0:{port}";
                    }

            }
            return new string[1] { hostUrl }; ;
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string[] urls)
        {
            var ihostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(urls);
                });
            var isService = !(Debugger.IsAttached || args.Contains("-c"));
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            if (isService)
            {
                ihostBuilder.UseWindowsService();
            }
            return ihostBuilder;
        }
    }


   


}
