using Hyperdrive.Storage.Domain.Exceptions;
using Hyperdrive.Storage.Infrastructure.Contexts;
using Hyperdrive.Storage.Infrastructure.Managers;
using Hyperdrive.Storage.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Hyperdrive.Storage.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class DriveItemManagerTest : BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{DriveItemManager}"/>
    /// </summary>
    private ILogger<DriveItemManager> Logger;

    /// <summary>
    /// Instance of <see cref="DriveItemManager"/>
    /// </summary>
    private DriveItemManager Manager;

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

        Manager = new DriveItemManager(Context, Logger);
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

        Logger = @loggerFactory.CreateLogger<DriveItemManager>();
    }

    [Test]
    public async Task FindDriveItemById()
    {
        await Manager.FindDriveItemById(new Guid("9bd5be02-f392-4ca1-923c-6c9753b645e5"));
        Assert.Pass();
    }

    [Test]
    public void FindDriveItemByFileName()
    {
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.FindDriveItemByFileName("Wanabe.mp3", new Guid("f0354da5-dd9e-4f70-8379-7a0f7dfc67ec"), 1)); //5
    }

    [Test]
    public async Task RemoveDriveItemById()
    {
        var @item = Context.DriveItems.First(x => x.Id == new Guid("9bd5be02-f392-4ca1-923c-6c9753b645e5"));

        await Manager.RemoveDriveItem(@item);
        Assert.Pass();
    }

    [Test]
    public async Task FindPaginatedDriveItemByApplicationUserId()
    {
        await Manager.FindPaginatedDriveItemByApplicationUserId(1, 15, 1, null);
        Assert.Pass();
    }

    [Test]
    public async Task FindPaginatedSharedDriveItemWithApplicationUserId()
    {
        await Manager.FindPaginatedSharedDriveItemWithApplicationUserId(1, 15, 5);
        Assert.Pass();
    }

    [Test]
    public async Task AddDriveItem()
    {
        await Manager.AddDriveItem(new Guid("9bd5be02-f392-4ca1-923c-6c9753b645e5"), false, 1);

        Assert.Pass();
    }

    [Test]
    public async Task AddSharedWith()
    {
        var @item = Context.DriveItems.First(x => x.Id == new Guid("9bd5be02-f392-4ca1-923c-6c9753b645e5"));

        var @users = new List<int>() { 2, 3 };

        await Manager.AddSharedWith(users, item);

        Assert.Pass();
    }

    [Test]
    public void CheckFileName()
    {
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.CheckFileName("Pictures", null, 1));
    }

    [Test]
    public void CheckName()
    {
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.CheckName("Pictures", new Guid("9bd5be02-f392-4ca1-923c-6c9753b645e5"), null, 1)); //1
    }

    [Test]
    public async Task ReloadDriveItemById()
    {
        await Manager.ReloadDriveItemById(new Guid("9bd5be02-f392-4ca1-923c-6c9753b645e5")); //1

        Assert.Pass();
    }
}