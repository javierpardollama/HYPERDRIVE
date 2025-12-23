using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemVersionManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class DriveItemVersionManagerTest : BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="DriveItemManager"/>
    /// </summary>
    private DriveItemVersionManager Manager;

    /// <summary>
    /// Instance of <see cref="ILogger{DriveItemVersionManager}"/>
    /// </summary>
    private ILogger<DriveItemVersionManager> Logger;

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

        Manager = new DriveItemVersionManager(Context, Logger);
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

        Logger = @loggerFactory.CreateLogger<DriveItemVersionManager>();
    }

    /// <summary>
    /// Finds Paginated Drive Item Version By Drive Item Id
    /// </summary>
    /// <returns>Instance of <see cref="Task}"/></returns>
    [Test]
    public async Task FindPaginatedDriveItemVersionByDriveItemId()
    {
        await Manager.FindPaginatedDriveItemVersionByDriveItemId(1, 15, 5);

        Assert.Pass();
    }

    /// <summary>
    /// Finds Drive Item Version By Id
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task FindDriveItemById()
    {
        await Manager.FindDriveItemVersionById(51);
        Assert.Pass();
    }

    /// <summary>
    /// Targets Drive Item Version
    /// </summary>
    /// <returns>Instance of <see cref="Task"/></returns>
    [Test]
    public async Task TargetDriveItemVersion()
    {
        var @entity = Context.DriveItemInfos.First(x => x.Id == 51);      

        await Manager.TargetDriveItemVersion(@entity);
        Assert.Pass();
    }

}
