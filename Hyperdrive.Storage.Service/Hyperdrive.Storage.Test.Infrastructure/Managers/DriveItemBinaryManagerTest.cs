using Hyperdrive.Storage.Infrastructure.Contexts;
using Hyperdrive.Storage.Infrastructure.Managers;
using Hyperdrive.Storage.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Hyperdrive.Storage.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemBinaryManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class DriveItemBinaryManagerTest : BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{DriveItemBinaryManager}"/>
    /// </summary>
    private ILogger<DriveItemBinaryManager> Logger;

    /// <summary>
    /// Instance of <see cref="DriveItemManager"/>
    /// </summary>
    private DriveItemBinaryManager Manager;

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

        Manager = new DriveItemBinaryManager(Context, Logger);
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

        Logger = @loggerFactory.CreateLogger<DriveItemBinaryManager>();
    }

    [Test]
    public async Task FindDriveItemBinaryById()
    {
        await Manager.FindDriveItemBinaryById(51);

        Assert.Pass();
    }


    [Test]
    public async Task FindLatestDriveItemBinaryById()
    {
        await Manager.FindLatestDriveItemBinaryById(5);

        Assert.Pass();
    }
}
