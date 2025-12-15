using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using NUnit.Framework;
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
    /// Sets Up
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        InstallServices();

        Context = new ApplicationContext(ContextOptionsBuilder.Options);
        Context.Seed();

        InstallHttpContext();


        Manager = new DriveItemVersionManager(Context);
    }

    [Test]
    public async Task FindPaginatedDriveItemVersionByDriveItemId()
    {
        await Manager.FindPaginatedDriveItemVersionByDriveItemId(1, 15, 5);

        Assert.Pass();
    }
   
}
