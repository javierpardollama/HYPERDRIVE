using Hyperdrive.Main.Domain.Entities;
using Hyperdrive.Main.Domain.Exceptions;
using Hyperdrive.Main.Infrastructure.Contexts;
using Hyperdrive.Main.Infrastructure.Managers;
using Hyperdrive.Main.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="ApplicationRoleManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class ApplicationRoleManagerTest : BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{ApplicationRoleManager}"/>
    /// </summary>
    private ILogger<ApplicationRoleManager> Logger;

    /// <summary>
    /// Instance of <see cref="ApplicationRoleManager"/>
    /// </summary>
    private ApplicationRoleManager Manager;

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

        Manager = new ApplicationRoleManager(Logger, RoleManager);
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

        Logger = @loggerFactory.CreateLogger<ApplicationRoleManager>();
    }

    /// <summary>
    /// Finds All Application Role
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindAllApplicationRole()
    {
        await Manager.FindAllApplicationRole();

        Assert.Pass();
    }


    /// <summary>
    /// Finds Paginated Application Role
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindPaginatedApplicationRole()
    {
        await Manager.FindPaginatedApplicationRole(1, 5);

        Assert.Pass();
    }

    /// <summary>
    /// Finds Application Role By Id
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindApplicationRoleById()
    {
        await Manager.FindApplicationRoleById(1);

        Assert.Pass();
    }

    /// <summary>
    /// Removes Application Role By Id
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task RemoveApplicationRoleById()
    {
        var @role = Context.Roles.First(x => x.Id == 2);

        await Manager.RemoveApplicationRole(@role);

        Assert.Pass();
    }

    /// <summary>
    /// Updates Application Role
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task UpdateApplicationRole()
    {
        var @role = new ApplicationRole
        {
            Id = 5,
            Name = "Princess",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            ImageUri = "URL/Princess_500px.png"
        };

        await Manager.UpdateApplicationRole(@role);

        Assert.Pass();
    }

    /// <summary>
    /// Adds Application Role
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task AddApplicationRole()
    {
        var @role = new ApplicationRole
        {
            Name = "Witch",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            ImageUri = "URL/Witch_500px.png"
        };

        await Manager.AddApplicationRole(@role);

        Assert.Pass();
    }

    /// <summary>
    /// Checks Name
    /// </summary>
    [Test]
    public void CheckName()
    {
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.CheckName("Paladin"));

        Assert.Pass();
    }
}