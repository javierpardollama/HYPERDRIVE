using System;
using System.Collections.Generic;

using AutoMapper;

using Hyperdrive.Tier.Contexts.Classes;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Mappings.Classes;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestBaseService"/> class.
    /// </summary>
    public abstract class TestBaseService
    {
        /// <summary>
        /// Instance of <see cref="IMapper"/>
        /// </summary>
        public IMapper Mapper;

        /// <summary>
        /// Instance of <see cref="IConfiguration"/>
        /// </summary>
        public IConfiguration Configuration;

        /// <summary>
        /// Instance of <see cref="Dictionary{string, string}"/>
        /// </summary>
        private Dictionary<string, string> JwtSettings;

        /// <summary>
        /// Instance of <see cref="DbContextOptions{ApplicationContext}"/>
        /// </summary>
        private DbContextOptions<ApplicationContext> Options;

        /// <summary>
        /// Instance of <see cref="ApplicationContext"/>
        /// </summary>
        public ApplicationContext Context;

        /// <summary>
        /// Instance of <see cref="UserManager{ApplicationUser}"/>
        /// </summary>
        public UserManager<ApplicationUser> UserManager;

        /// <summary>
        /// Instance of <see cref="SignInManager{ApplicationUser}"/>
        /// </summary>
        public SignInManager<ApplicationUser> SignInManager;

        /// <summary>
        /// Instance of <see cref="ServiceCollection"/>
        /// </summary>
        private ServiceCollection Services;

        /// <summary>
        /// Instance of <see cref="ServiceProvider"/>
        /// </summary>
        private ServiceProvider ServiceProvider;

        /// <summary>
        /// Sets Up Services
        /// </summary>
        public void SetUpServices()
        {
            Services = new ServiceCollection();

            Services
                .AddSingleton(Configuration)
                .AddDbContext<ApplicationContext>(o => o.UseSqlite("Data Source=Hyperdrive.db"))
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Lockout = new LockoutOptions()
                    {
                        AllowedForNewUsers = true,
                        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                        MaxFailedAccessAttempts = 5
                    };
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            Services.AddLogging();

            ServiceProvider = Services.BuildServiceProvider();

            Context = new ApplicationContext(Options);
            UserManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            SignInManager = ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        /// <summary>
        /// Sets Up Mapper
        /// </summary>
        public void SetUpMapper()
        {
            MapperConfiguration @config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelingProfile());
            });

            Mapper = @config.CreateMapper();
        }

        /// <summary>
        /// Sets Up Jwt Settings
        /// </summary>
        public void SetUpJwtSettings()
        {
            JwtSettings = new Dictionary<string, string>
            {
                { "Jwt:JwtKey", "SOME_RANDOM_KEY_DO_NOT_SHARE"},
                { "Jwt:JwtIssuer", "http://localhost:15208"},
                { "Jwt:JwtAudience", " http://localhost:4200"},
                { "Jwt:JwtExpireDays", "30"},
            };
        }

        /// <summary>
        /// Sets Up Configuration
        /// </summary>
        public void SetUpConfiguration()
        {
            Configuration = new ConfigurationBuilder().AddInMemoryCollection(JwtSettings).Build();
        }

        /// <summary>
        /// Sets Up Options
        /// </summary>
        public void SetUpOptions()
        {
            Options = new DbContextOptionsBuilder<ApplicationContext>()
           .UseInMemoryDatabase(databaseName: "Data Source=Hyperdrive.db")
           .Options;
        }
    }
}