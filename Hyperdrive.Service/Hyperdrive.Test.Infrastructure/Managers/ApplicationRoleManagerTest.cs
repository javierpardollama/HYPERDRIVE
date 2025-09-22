using System;
using System.Linq;
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

            Seed();

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
        /// Seeds
        /// </summary>
        private void Seed()
        {
            if (!Context.Roles.Any())
            {
                Context.Roles.Add(new ApplicationRole
                {
                    Id = 1,
                    Name = "Dungeon Master",
                    NormalizedName = "DUNGEON_MASTER",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Dungeon_Master_500px.png"
                });
                Context.Roles.Add(new ApplicationRole
                {
                    Id = 2,
                    Name = "Paladin",
                    NormalizedName = "PALADIN",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Paladin_500px.png"
                });
                Context.Roles.Add(new ApplicationRole
                {
                    Id = 3,
                    Name = "Sorceress",
                    NormalizedName = "SORCERESS",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Sorceress_2_500px.png"
                });
                Context.Roles.Add(new ApplicationRole
                {
                    Id = 4,
                    Name = "Rogue",
                    NormalizedName = "ROGUE",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Rogue_2_500px.png"
                });
                Context.Roles.Add(new ApplicationRole
                {
                    Id = 5,
                    Name = "Bard",
                    NormalizedName = "BARD",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Bard_500px.png"
                });
            }

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
                Context.Users.Add(new ApplicationUser
                {
                    Id = 4,
                    FirstName = "Genesis",
                    LastName = "Gavin",
                    UserName = "genesis.gavin",
                    Email = "genesis.gavin@email.com",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                });
            }

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