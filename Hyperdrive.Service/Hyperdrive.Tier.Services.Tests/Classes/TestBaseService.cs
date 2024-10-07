using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

using AutoMapper;

using Hyperdrive.Tier.Contexts.Classes;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Mappings.Classes;
using Hyperdrive.Tier.Settings.Classes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Sandwitch.Tier.Contexts.Interceptors;

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
        protected IMapper Mapper;

        /// <summary>
        /// Instance of <see cref="IOptions{JwtSettings}"/>
        /// </summary>
        protected IOptions<JwtSettings> JwtOptions;

        /// <summary>
        /// Instance of <see cref="DbContextOptions{ApplicationContext}"/>
        /// </summary>
        protected DbContextOptions<ApplicationContext> ContextOptions;

        /// <summary>
        /// Instance of <see cref="ApplicationContext"/>
        /// </summary>
        protected ApplicationContext Context;

        /// <summary>
        /// Instance of <see cref="UserManager{ApplicationUser}"/>
        /// </summary>
        protected UserManager<ApplicationUser> UserManager;

        /// <summary>
        /// Instance of <see cref="RoleManager{ApplicationRole}"/>
        /// </summary>
        protected RoleManager<ApplicationRole> RoleManager;

        /// <summary>
        /// Instance of <see cref="SignInManager{ApplicationUser}"/>
        /// </summary>
        protected SignInManager<ApplicationUser> SignInManager;

        /// <summary>
        /// Instance of <see cref="ServiceCollection"/>
        /// </summary>
        protected ServiceCollection Services;

        /// <summary>
        /// Instance of <see cref="ServiceProvider"/>
        /// </summary>
        protected ServiceProvider ServiceProvider;

        /// <summary>
        /// Sets Up Services
        /// </summary>
        public void SetUpServices()
        {
            Services = new ServiceCollection();

            Services
                .AddLogging()
                .AddDbContext<ApplicationContext>(o => o.UseInMemoryDatabase("hyperdrive.db"))
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();


            ServiceProvider = Services.BuildServiceProvider();
            Context = new ApplicationContext(ContextOptions);
            UserManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager = ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            SignInManager = ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        /// <summary>
        /// Sets Up Http Context
        /// </summary>
        public void SetUpHttpContext()
        {
            Services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor() { HttpContext = new DefaultHttpContext() { RequestServices = Services.BuildServiceProvider() } });
        }

        /// <summary>
        /// Sets Up Mapper
        /// </summary>
        public void SetUpMapper()
        {
            MapperConfiguration @config = new(cfg =>
            {
                cfg.AddProfile(new ModelingProfile());
            });

            Mapper = @config.CreateMapper();
        }

        /// <summary>
        /// Sets Up Jwt Options
        /// </summary>
        public void SetUpJwtOptions() => JwtOptions = Options.Create(new JwtSettings()
        {
            JwtAudiences = new List<string>() { "https://localhost:4200" },
            JwtExpireMinutes = 60,
            JwtIssuer = "https://localhost:15208",
            JwtAuthority = "https://localhost:15208",
            JwtKey = "These are not the droids who you are looking for"
        });

        /// <summary>
        /// Sets Up Context Options
        /// </summary>
        public void SetUpContextOptions() => ContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
           .UseInMemoryDatabase("hyperdrive.db")
           .Options;
    }
}