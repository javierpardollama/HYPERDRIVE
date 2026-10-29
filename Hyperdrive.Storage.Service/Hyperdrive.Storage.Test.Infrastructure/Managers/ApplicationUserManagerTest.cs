using Hyperdrive.Storage.Domain.Entities;
using Hyperdrive.Storage.Infrastructure.Contexts;
using Hyperdrive.Storage.Infrastructure.Managers;
using Hyperdrive.Storage.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Hyperdrive.Storage.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="ApplicationUserManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class ApplicationUserManagerTest : BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{TCategoryName}"/>
    /// </summary>
    private ILogger<ApplicationUserManager> Logger;

    /// <summary>
    /// Instance of <see cref="ApplicationUserManager"/>
    /// </summary>
    private ApplicationUserManager Manager;

    /// <summary>
    /// Sets Up
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        InstallServices();

        Context = new ApplicationContext(ContextOptionsBuilder.Options);
        Context.Seed();

        InstallLogger();

        Manager = new ApplicationUserManager(Context, Logger);
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

        Logger = @loggerFactory.CreateLogger<ApplicationUserManager>();
    }

    /// <summary>
    /// Finds Application User By Id
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindApplicationUserById()
    {
        await Manager.FindApplicationUserById(1);

        Assert.Pass();
    }

    /// <summary>
    /// Finds Application User By Ids
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindAllApplicationUserByIds()
    {
        await Manager.FindAllApplicationUserByIds([1, 2]);

        Assert.Pass();
    }
    
    /// <summary>
    /// Removes Application User By Id
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task RemoveApplicationUserById()
    {
        await Manager.ReloadApplicationUserById(1);

        Assert.Pass();
    }
    
    /// <summary>
    /// Updates Application Role
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task UpdateApplicationRole()
    {
        var @user = new ApplicationUser
        {
            Id = 1,
            FirstName = "Orlando",
            LastName = "Dawson"
        };

        await Manager.UpdateApplicationUser(@user);

        Assert.Pass();
    }

    /// <summary>
    /// Adds Application User
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task AddApplicationUser()
    {
        var @user = new ApplicationUser
        {
           Id = 7,
           FirstName = "Leah",
           LastName = "Stokes"
        };

        await Manager.AddApplicationUser(@user);

        Assert.Pass();
    }
    
    
    /// <summary>
    /// Reloads Application User By Id
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task ReloadApplicationUserById()
    {
        await Manager.ReloadApplicationUserById(1);

        Assert.Pass();
    }
}