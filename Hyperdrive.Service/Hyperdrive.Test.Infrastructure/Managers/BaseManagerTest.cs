using System;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Settings;
using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="BaseManagerTest" /> class.
/// </summary>
public abstract class BaseManagerTest
{
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
    ///     Gets or Sets <see cref="IOptions{ApiSettings}" />
    /// </summary>
    protected IOptions<JwtSettings> ApiOptions { get; set; } = Options.Create(new JwtSettings
    {
        JwtAudiences = ["https://localhost:4200"],
        JwtAuthority = "https://localhost:7297",
        JwtIssuer = "https://localhost:7297",
        JwtExpireDays = 2,
        JwtExpireMinutes = 15,
        JwtKey = "I've got a bad feeling about this"
    });

    /// <summary>
    ///     Gets or Sets <see cref="ApplicationContext" />
    /// </summary>
    protected ApplicationContext Context { get; private set; }

    /// <summary>
    /// Gets or Sets <see cref="ServiceCollection"/>
    /// </summary>
    private ServiceCollection Services { get; } = [];

    /// <summary>
    /// Instance of <see cref="ServiceProvider"/>
    /// </summary>
    private ServiceProvider ServiceProvider;

    /// <summary>
    /// Gets or Sets <see cref="ContextOptionsAction"/>
    /// </summary>
    private readonly Action<DbContextOptionsBuilder> ContextOptionsAction = options =>
    {
        options.UseInMemoryDatabase("hyperdrive.db");
        options.AddInterceptors(new SoftDeleteInterceptor());
        options.EnableSensitiveDataLogging();
    };

    /// <summary>
    /// Install Services
    /// </summary>
    public void InstallServices()
    {
        Services
            .AddLogging()
            .AddDbContext<ApplicationContext>(ContextOptionsAction)
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

        ServiceProvider = Services.BuildServiceProvider();
        Context = ServiceProvider.GetRequiredService<ApplicationContext>();
        UserManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager = ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        SignInManager = ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
    }

    /// <summary>
    ///Installs Http Context
    /// </summary>
    public void InstallHttpContext()
    {
        Services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor()
            { HttpContext = new DefaultHttpContext() { RequestServices = ServiceProvider } });
    }
}