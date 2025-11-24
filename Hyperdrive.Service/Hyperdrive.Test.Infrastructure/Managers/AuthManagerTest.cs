using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="ApplicationUserManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class AuthManagerTest : BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{AuthManager}"/>
    /// </summary>
    private ILogger<AuthManager> Logger;

    /// <summary>
    /// Instance of <see cref="AuthManager"/>
    /// </summary>
    private AuthManager Manager;

    /// <summary>
    /// Sets Up
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        InstallServices();

        Context = new ApplicationContext(ContextOptionsBuilder.Options);
        Context.Seed();

        InstallHttpContext();

        InstallLogger();      

        Manager = new AuthManager(Logger, UserManager, SignInManager);
    }

    /// <summary>
    /// Installs Logger
    /// </summary>
    private void InstallLogger()
    {
        ILoggerFactory @loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole();
        });

        Logger = @loggerFactory.CreateLogger<AuthManager>();
    }   

    /// <summary>
    /// Joins In
    /// </summary>
    [Test]
    public async Task JoinIn()
    {
        await Manager.JoinIn("monique.genaro@mail.com", "P@ssw0rd");

        Assert.Pass();
    }
    
    /// <summary>
    /// Signs In
    /// </summary>
    [Test]
    public void SignIn()
    {
        var @user = new ApplicationUser
        {
            Id = 4,
            FirstName = "Genesis",
            LastName = "Gavin",
            UserName = "genesis.gavin",
            Email = "genesis.gavin@email.com",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };
        
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Manager.SignIn(user, "genesis.gavin@email.com", "P@ssw0rd"));
    }

    /// <summary>
    /// Signs Out
    /// </summary>
    [Test]
    public async Task SignOut()
    {
        await Manager.SignOut("monique.genaro@mail.com");

        Assert.Pass();
    }
}