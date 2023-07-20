using System;
using System.Linq;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Updates;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestApplicationRoleService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestApplicationRoleService : TestBaseService
    {
        /// <summary>
        /// Instance of <see cref="ILogger{ApplicationRoleService}"/>
        /// </summary>
        private ILogger<ApplicationRoleService> Logger;

        /// <summary>
        /// Instance of <see cref="ApplicationRoleService"/>
        /// </summary>
        private ApplicationRoleService Service;

        /// <summary>
        /// Initializes a new Instance of <see cref="TestApplicationRoleService"/>
        /// </summary>
        public TestApplicationRoleService()
        {
           
        }

        /// <summary>
        /// Sets Up
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SetUpContextOptions();

            SetUpJwtOptions();

            SetUpServices();

            SetUpMapper();

            SetUpLogger();

            SetUpContext();

            Service = new ApplicationRoleService(Mapper, Context, Logger);          
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Context.Users.RemoveRange(Context.Users.ToList());
            Context.Roles.RemoveRange(Context.Roles.ToList());

            Context.SaveChanges();
        }

        /// <summary>
        /// Sets Up Logger
        /// </summary>
        private void SetUpLogger()
        {
            ILoggerFactory @loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole();
            });

            Logger = @loggerFactory.CreateLogger<ApplicationRoleService>();
        }

        /// <summary>
        /// Sets Up Context
        /// </summary>
        private void SetUpContext()
        {
            Context.Roles.Add(new ApplicationRole { Name = "Role 1", LastModified = DateTime.Now, Deleted = false, ImageUri = "URL/Role_1_500px.png" });
            Context.Roles.Add(new ApplicationRole { Name = "Role 2", LastModified = DateTime.Now, Deleted = false, ImageUri = "URL/Role_2_500px.png" });
            Context.Roles.Add(new ApplicationRole { Name = "Role 3", LastModified = DateTime.Now, Deleted = false, ImageUri = "URL/Role_3_500px.png" });

            Context.SaveChanges();
        }

        /// <summary>
        /// Finds All Application Role
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindAllApplicationRole()
        {
            await Service.FindAllApplicationRole();

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application Role By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindApplicationRoleById()
        {
            await Service.FindApplicationRoleById(Context.Roles.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Removes Application Role By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task RemoveApplicationRoleById()
        {
            await Service.RemoveApplicationRoleById(Context.Roles.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Updates Application Role
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task UpdateApplicationRole()
        {
            UpdateApplicationRole @ViewModel = new()
            {
                Id = Context.Roles.FirstOrDefault().Id,
                Name = "Role 21",
                ImageUri = "URL/Role_21_500px.png",
            };

            await Service.UpdateApplicationRole(@ViewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Adds Application Role
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task AddApplicationRole()
        {
            AddApplicationRole @ViewModel = new()
            {
                Name = "Role 41",
                ImageUri = "URL/Role_41_500px.png",
            };

            await Service.AddApplicationRole(@ViewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Checks Name
        /// </summary>
        [Test]
        public void CheckName()
        {
            AddApplicationRole @ViewModel = new()
            {
                Name = "Role 2",
                ImageUri = "URL/Role_2_500px.png",
            };

            Exception exception = Assert.ThrowsAsync<Exception>(async () => await Service.CheckName(@ViewModel));

            Assert.Pass();
        }
    }
}
