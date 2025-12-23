using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemInfoManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class DriveItemInfoManagerTest: BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="DriveItemManager"/>
    /// </summary>
    private DriveItemInfoManager Manager;

    /// <summary>
    /// Instance of <see cref="ILogger{DriveItemInfoManager}"/>
    /// </summary>
    private ILogger<DriveItemInfoManager> Logger;

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

        Manager = new DriveItemInfoManager(Context, Logger);
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

        Logger = @loggerFactory.CreateLogger<DriveItemInfoManager>();
    }
    
    [Test]
    public async Task AddAsFileNameInfo()
    {        
        await Manager.AddAsFileNameInfo(5, "Wanabe.mp3");

        Assert.Pass();
    }
    
    [Test]
    public async Task AddAsNameActivity()
    {      
        await Manager.AddAsNameInfo(5, "Spice Girls - Wannabe", "mp3");

        Assert.Pass();
    }
    
}