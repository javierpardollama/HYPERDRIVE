using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Managers;

namespace Hyperdrive.Test.Infrastructure.Managers
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
    /// </summary>
    [TestFixture]
    public class ApplicationUserManagerTest : BaseManagerTest
    {
        /// <summary>
        /// Instance of <see cref="ILogger{TCategoryName}"/>
        /// </summary>
        private ILogger<ApplicationUserManager> Logger;

        /// <summary>
        /// Instance of <see cref="ApplicationUserManager"/>
        /// </summary>
        private ApplicationUserManager Manager;

        /// <summary>
        /// Sets Up
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            InstallServices();

            InstallLogger();

            Seed();

            Manager = new ApplicationUserManager(Logger, UserManager);
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

            Logger = @loggerFactory.CreateLogger<ApplicationUserManager>();
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
                    ImageUri = "URL/Role_2_500px.png"
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
        /// Finds All Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindAllApplicationUser()
        {
            await Manager.FindAllApplicationUser();

            Assert.Pass();
        }

        /// <summary>
        /// Finds Paginated Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindPaginatedApplicationUser()
        {
            await Manager.FindPaginatedApplicationUser(1, 5);

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindApplicationUserById()
        {
            await Manager.FindApplicationUserById(1);

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindApplicationUserByEmail()
        {
            await Manager.FindApplicationUserByEmail("stafford.parker@email.com");

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application User By Ids
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindAllApplicationUserByIds()
        {
            await Manager.FindAllApplicationUserByIds([1, 2]);

            Assert.Pass();
        }

        /// <summary>
        /// Removes Application User By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task RemoveApplicationUserById()
        {
            await Manager.RemoveApplicationUserById(2);

            Assert.Pass();
        }

        /// <summary>
        /// Adds Application Roles to Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task AddApplicationUserRoles()
        {
            var @user = await Manager.FindApplicationUserById(3);

            var @roles = new List<string>() { "Rogue", "Bard" };

            await Manager.AddApplicationUserRoles(roles, user);

            Assert.Pass();
        }

        /// <summary>
        /// Reloads Application User By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task ReloadApplicationUserById()
        {
            await Manager.ReloadApplicationUserById(4);

            Assert.Pass();
        }
    }
}