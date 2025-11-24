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

        InstallHttpContext();

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
        await Manager.FindDriveItemById(1);
        Assert.Pass();
    }

    [Test]
    public void FindDriveItemByFileName()
    {       
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.FindDriveItemByFileName("Wanabe.mp3", 5, 1));       
    }

    [Test]
    public async Task RemoveDriveItemById()
    {
        var @item = Context.DriveItems.First(x => x.Id == 1);

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
    public async Task FindAllDriveItemVersionByDriveItemId()
    {
        await Manager.FindAllDriveItemVersionByDriveItemId(1);
        Assert.Pass();
    }

    [Test]
    public async Task AddDriveItem()
    {      
        await Manager.AddDriveItem("Everybody.mp3", 1, false, 1);

        Assert.Pass();
    }

    [Test]
    public async Task AddSharedWith()
    {
        var @item = Context.DriveItems.First(x => x.Id == 1);

        var @users = Context.Users.Where(x => new List<int>() { 2, 3 }.Contains(x.Id)).ToList();

        await Manager.AddSharedWith(users, item);

        Assert.Pass();
    }

    [Test]
    public async Task AddAsFileNameActivity()
    {        
        await Manager.AddAsFileNameActivity(5, "Wanabe.mp3", "audio/mpeg", 120, "72AQjWn/vBsFvWD+K1c3IA==");

        Assert.Pass();
    }

    [Test]
    public async Task AddAsNameActivity()
    {      
        await Manager.AddAsNameActivity(5, "Spice Girls - Wannabe", "mp3");

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
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.CheckName("Pictures", 1, null, 1));
    }

    [Test]
    public async Task FindDriveItemBinaryById()
    {
        await Manager.FindDriveItemBinaryById(5);

        Assert.Pass();
    }

    [Test]
    public async Task ReloadDriveItemById()
    {
        await Manager.ReloadDriveItemById(1);

        Assert.Pass();
    }
}