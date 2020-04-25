using System;

using Hyperdrive.Tier.Contexts.Classes;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace Hyperdrive.Tier.Web
{
    /// <summary>
    /// Represents a <see cref="Program"/> class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">Injected <see cref="string[]"/></param>
        public static void Main(string[] args)
        {
            using IWebHost host = BuildWebHost(args);
            ApplyWebHostMigrations(host.Services);

            host.Run();
        }

        /// <summary>
        /// Builds WebHost
        /// </summary>
        /// <param name="args">Injected <see cref="string[]"/></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration))
            .Build();

        /// <summary>
        /// Applies WebHost Migrations
        /// </summary>
        /// <param name="serviceProvider">Injected <see cref="IServiceProvider"/></param>
        public static void ApplyWebHostMigrations(IServiceProvider serviceProvider)
        {
            using IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;

            using ApplicationContext applicationContext = scopeServiceProvider.GetService<ApplicationContext>();
            applicationContext.Database.Migrate();
        }
    }
}
