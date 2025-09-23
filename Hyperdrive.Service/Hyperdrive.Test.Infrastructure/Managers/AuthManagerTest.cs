using System;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Infrastructure.Managers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

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

        InstallHttpContext();

        InstallLogger();

        Seed();

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
    /// Seeds
    /// </summary>
    private void Seed()
    {
        if (!Context.Users.Any())
        {
            Context.Users.Add(new ApplicationUser
            {
                Id = 1,
                FirstName = "Stafford",
                LastName = "Parker",
                UserName = "stafford.parker",
                Email = "stafford.parker@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
            Context.Users.Add(new ApplicationUser
            {
                Id = 2,
                FirstName = "Dee",
                LastName = "Sandy",
                UserName = "dee.sandy",
                Email = "dee.sandy@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
            Context.Users.Add(new ApplicationUser
            {
                Id = 3,
                FirstName = "Orinda Navy",
                LastName = "Navy",
                UserName = "orinda.navy",
                Email = "orinda.navy@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
            Context.Users.Add(new ApplicationUser
            {
                Id = 4,
                FirstName = "Genesis",
                LastName = "Gavin",
                UserName = "genesis.gavin",
                Email = "genesis.gavin@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
        }

        Context.SaveChanges();
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
            LastModified = DateTime.UtcNow,
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