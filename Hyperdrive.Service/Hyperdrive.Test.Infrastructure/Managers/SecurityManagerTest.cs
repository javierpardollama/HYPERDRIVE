using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="SecurityManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class SecurityManagerTest: BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{SecurityManager}"/>
    /// </summary>
    private ILogger<SecurityManager> Logger;

    /// <summary>
    /// Instance of <see cref="SecurityManager"/>
    /// </summary>
    private SecurityManager Manager;

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

        Manager = new SecurityManager(UserManager, Logger);
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

        Logger = @loggerFactory.CreateLogger<SecurityManager>();
    }   

    /// <summary>
    /// Resets Password
    /// </summary>
    [Test]
    public void ResetPassword()
    {
        var @user = new ApplicationUser
        {
            Id = 1,
            FirstName = "Stafford",
            LastName = "Parker",
            UserName = "stafford.parker",
            Email = "stafford.parker@email.com",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };
        
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Manager.ResetPassword(user, "P@ssw0rd"));
    }

    /// <summary>
    /// Changes Password
    /// </summary>
    [Test]
    public void ChangePassword()
    {
        var @user = new ApplicationUser
        {
            Id = 2,
            FirstName = "Dee",
            LastName = "Sandy",
            UserName = "dee.sandy",
            Email = "dee.sandy@email.com",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };
        
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Manager.ChangePassword(user, "dee.sandy@email.com", "P@ssw0rd"));
    }

    /// <summary>
    /// Changes Phone Number
    /// </summary>
    [Test]
    public void ChangePhoneNumber()
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
        
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Manager.ChangePhoneNumber(user, "657890123"));
    }

    /// <summary>
    /// Changes Name
    /// </summary>
    [Test]
    public void ChangeName()
    {
        var @user = new ApplicationUser
        {
            Id = 5,
            FirstName = "Antonietta ",
            LastName = "Miranda",
            UserName = "antonietta.miranda",
            Email = "antonietta.miranda@email.com",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };
        
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Manager.ChangeName(user, "Antonietta Maria", "Miranda"));
    }
    
    /// <summary>
    /// Changes Email
    /// </summary>
    [Test]
    public void ChangeEmail()
    {
        var @user = new ApplicationUser
        {
            Id = 6,
            FirstName = "Alessa",
            LastName = "Simona",
            UserName = "alessa.simona",
            Email = "alessa.simona@email.com",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };
        
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Manager.ChangeEmail(user, "alessa.simona@new.email.com"));
    }
}