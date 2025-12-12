using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Infrastructure.Managers
{
    public class DriveItemVersionManagerTest : BaseManagerTest
    {
        /// <summary>
        /// Instance of <see cref="ILogger{DriveItemVersionManager}"/>
        /// </summary>
        private ILogger<DriveItemVersionManager> Logger;

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

        [Test]
        public async Task FindPaginatedDriveItemVersionByDriveItemId()
        {
            await Manager.FindPaginatedDriveItemVersionByDriveItemId(1, 15, 5);

            Assert.Pass();
        }

        [Test]
        public async Task FindLatestDriveItemBinaryById()
        {
            await Manager.FindDriveItemBinaryById(51);

            Assert.Pass();
        }
    }
}
