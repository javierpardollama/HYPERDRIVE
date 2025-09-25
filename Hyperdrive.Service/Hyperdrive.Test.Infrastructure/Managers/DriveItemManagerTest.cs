using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Infrastructure.Managers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

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

        InstallHttpContext();

        InstallLogger();

        Seed();

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
        }

        Context.SaveChanges();

        if (!Context.DriveItems.Any())
        {
            Context.DriveItems.Add(new DriveItem
            {
                Id = 2,
                Name = "Documents",
                NormalizedName = "DOCUMENTS",
                FileName = "Documents",
                NormalizedFileName = "DOCUMENTS",
                Folder = true,
                By = Context.Users.First(x => x.Id == 1),
                Parent = null,
                LastModified = DateTime.UtcNow,
                Deleted = false,
            });
            Context.DriveItems.Add(new DriveItem
            {
                Id = 3,
                Name = "Music",
                NormalizedName = "MUSIC",
                FileName = "Music",
                NormalizedFileName = "MUSIC",
                Folder = true,
                By = Context.Users.First(x => x.Id == 1),
                Parent = null,
                LastModified = DateTime.UtcNow,
                Deleted = false,
            });
            Context.DriveItems.Add(new DriveItem
            {
                Id = 4,
                Name = "Pictures",
                NormalizedName = "PICTURES",
                FileName = "Pictures",
                NormalizedFileName = "PICTURES",
                Folder = true,
                By = Context.Users.First(x => x.Id == 1),
                Parent = null,
                LastModified = DateTime.UtcNow,
                Deleted = false,
            });
            Context.DriveItems.Add(new DriveItem
            {
                Id = 5,
                Name = "Wanabe",
                NormalizedName = "WANABE",
                FileName = "Wanabe.mp3",
                NormalizedFileName = "WANABE.MP3",
                Extension = "mp3",
                NormalizedExtension = "MP3",
                Folder = true,
                By = Context.Users.First(x => x.Id == 1),
                Parent = new DriveItem
                {
                    Id = 1,
                    Name = "Shared",
                    NormalizedName = "SHARED",
                    FileName = "Shared",
                    NormalizedFileName = "SHARED",
                    Folder = true,
                    By = Context.Users.First(x => x.Id == 1),
                    Parent = null,
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                },
                LastModified = DateTime.UtcNow,
                Deleted = false,
            });
        }

        Context.SaveChanges();

        if (!Context.ApplicationUserDriveItems.Any())
        {
            Context.ApplicationUserDriveItems.Add(new ApplicationUserDriveItem
            {
                DriveItem = Context.DriveItems.First(x => x.Id == 5),
                ApplicationUser = Context.Users.First(x => x.Id == 3),
                LastModified = DateTime.UtcNow,
                Deleted = false,
            });

            Context.ApplicationUserDriveItems.Add(new ApplicationUserDriveItem
            {
                DriveItem = Context.DriveItems.First(x => x.Id == 5),
                ApplicationUser = Context.Users.First(x => x.Id == 3),
                LastModified = DateTime.UtcNow,
                Deleted = false,
            });
        }

        Context.SaveChanges();
    }

    [Test]
    public async Task FindDriveItemById()
    {
        await Manager.FindDriveItemById(1);
        Assert.Pass();
    }

    [Test]
    public async Task RemoveDriveItemById()
    {
        await Manager.RemoveDriveItemById(1);
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
        var @user = Context.Users.First(x => x.Id == 1);

        await Manager.AddDriveItem("Everybody.mp3", 1, false, @user);

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
    public async Task AddActivity()
    {
        var @item = Context.DriveItems.First(x => x.Id == 5);

        await Manager.AddActivity(item, "audio/mpeg", 120, "72AQjWn/vBsFvWD+K1c3IA==");

        Assert.Pass();
    }

    [Test]
    public async Task ChangeName()
    {
        await Manager.ChangeName("Spice Girls - Wannabe", "mp3", 5, 1);

        Assert.Pass();
    }

    [Test]
    public void CheckFileName()
    {
        Assert.ThrowsAsync<ServiceException>(async () => await Manager.CheckFileName("Pictures", null));
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