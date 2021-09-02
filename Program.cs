using DutchTreatEmpty.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreatEmpty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Length == 1 && args[0].ToLower() == "/seed")
            {
                RunningSeeds(host);
            }
            else
            {
                host.Run();
            }
        }

        private static void RunningSeeds(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(AppConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void AppConfiguration(HostBuilderContext cntx,
            IConfigurationBuilder bldr)
        {
            bldr.Sources.Clear();
            bldr.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }
    }
}
