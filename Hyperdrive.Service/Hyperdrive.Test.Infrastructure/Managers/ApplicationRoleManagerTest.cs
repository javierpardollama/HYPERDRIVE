using System;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Infrastructure.Managers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Hyperdrive.Test.Infrastructure.Managers
{
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

            InstallLogger();

            SetUpData();

            Manager = new ApplicationRoleManager(Logger, RoleManager);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
           
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
        /// Sets Up Data
        /// </summary>
        private void SetUpData()
        {
            Context.Roles.Add(new ApplicationRole
            {
                Id = 1,
                Name = "Dungeon Master", 
                LastModified = DateTime.UtcNow, 
                Deleted = false, 
                ImageUri = "URL/Dungeon_Master_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 2,
                Name = "Paladin", 
                LastModified = DateTime.UtcNow, 
                Deleted = false, 
                ImageUri = "URL/Paladin_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 3,
                Name = "Sorceress", 
                LastModified = DateTime.UtcNow, 
                Deleted = false, 
                ImageUri = "URL/Role_2_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 4,
                Name = "Rogue", 
                LastModified = DateTime.UtcNow, 
                Deleted = false, 
                ImageUri = "URL/Rogue_2_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 5,
                Name = "Bard", 
                LastModified = DateTime.UtcNow, 
                Deleted = false, 
                ImageUri = "URL/Bard_500px.png"
            });
            
            Context.SaveChanges();
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
            await Manager.FindPaginatedApplicationRole( 1, 5);

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
            await Manager.RemoveApplicationRoleById(2);

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
                LastModified = DateTime.UtcNow, 
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
                LastModified = DateTime.UtcNow, 
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
}