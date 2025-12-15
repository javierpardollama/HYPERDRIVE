using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Infrastructure.Managers;

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

        Manager = new ApplicationUserManager(Logger, UserManager);
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
    /// Finds All Application User
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindAllApplicationUser()
    {
        await Manager.FindAllApplicationUser();

        Assert.Pass();
    }

    /// <summary>
    /// Finds Paginated Application User
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindPaginatedApplicationUser()
    {
        await Manager.FindPaginatedApplicationUser(1, 5);

        Assert.Pass();
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
    /// Finds Application User By Email
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindApplicationUserByEmail()
    {
        await Manager.FindApplicationUserByEmail("stafford.parker@email.com");

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
        var @user = Context.Users.First(x => x.Id == 1);

        await Manager.RemoveApplicationUser(@user);

        Assert.Pass();
    }

    /// <summary>
    /// Adds Application Roles to Application User
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task AddApplicationUserRoles()
    {
        var @user = Context.Users.First(x => x.Id == 3);

        var @roles = new List<string>() { "Rogue", "Bard" };

        await Manager.AddApplicationUserRoles(roles, user);

        Assert.Pass();
    }

    /// <summary>
    /// Checks Email
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task CheckEmail()
    {
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.CheckEmail("stafford.parker@email.com"));
    }

    /// <summary>
    /// Reloads Application User By Id
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task ReloadApplicationUserById()
    {
        await Manager.ReloadApplicationUserById(4);

        Assert.Pass();
    }
}