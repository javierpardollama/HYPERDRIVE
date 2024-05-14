using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestApplicationUserService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestApplicationUserService : TestBaseService
    {
        /// <summary>
        /// Instance of <see cref="ILogger{ApplicationUserService}"/>
        /// </summary>
        private ILogger<ApplicationUserService> Logger;

        /// <summary>
        /// Instance of <see cref="ApplicationUserService"/>
        /// </summary>
        private ApplicationUserService Service;

        /// <summary>
        /// Initializes a new Instance of <see cref="TestApplicationUserService"/>
        /// </summary>
        public TestApplicationUserService()
        {

        }

        /// <summary>
        /// Sets Up
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetUpContextOptions();

            SetUpJwtOptions();

            SetUpServices();

            SetUpMapper();

            SetUpLogger();

            SetUpContext();

            Service = new ApplicationUserService(Mapper, Context, Logger);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
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

            Logger = @loggerFactory.CreateLogger<ApplicationUserService>();
        }

        /// <summary>
        /// Sets Up Context
        /// </summary>
        private void SetUpContext()
        {
            Context.Users.Add(new ApplicationUser { FirstName = "First", LastName = "User", UserName = "firstuser@email.com", Email = "firstuser@email.com", LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.Users.Add(new ApplicationUser { FirstName = "Second", LastName = "User", UserName = "seconduser@email.com", Email = "seconduser@email.com", LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.Users.Add(new ApplicationUser { FirstName = "Thirst", LastName = "User", UserName = "thirstuser@email.com", Email = "thirstuser@email.com", LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });

            Context.Roles.Add(new ApplicationRole { Name = "Role 1", LastModified = DateTime.Now, Deleted = false, ImageUri = "URL/Role_1_500px.png" });
            Context.Roles.Add(new ApplicationRole { Name = "Role 2", LastModified = DateTime.Now, Deleted = false, ImageUri = "URL/Role_2_500px.png" });
            Context.Roles.Add(new ApplicationRole { Name = "Role 3", LastModified = DateTime.Now, Deleted = false, ImageUri = "URL/Role_3_500px.png" });

            Context.SaveChanges();
        }

        /// <summary>
        /// Finds All Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindAllApplicationUser()
        {
            await Service.FindAllApplicationUser();

            Assert.Pass();
        }

        /// <summary>
        /// Finds Paginated Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindPaginatedApplicationUser()
        {
            await Service.FindPaginatedApplicationUser(new FilterPageApplicationUser { Index = 1, Size = 5, ApplicationUserId = Context.Roles.FirstOrDefault().Id });

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindApplicationUserById()
        {
            await Service.FindApplicationUserById(Context.Users.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Removes Application User By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task RemoveApplicationUserById()
        {
            await Service.RemoveApplicationUserById(Context.Users.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Updates Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task UpdateApplicationUser()
        {
            UpdateApplicationUser viewModel = new()
            {
                Id = Context.Users.FirstOrDefault().Id,
                ApplicationRolesId = new List<int> { Context.Users.FirstOrDefault().Id }
            };

            await Service.UpdateApplicationUser(viewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Updates Application User Role
        /// </summary>
        [Test]
        public void UpdateApplicationUserRole()
        {
            UpdateApplicationUser viewModel = new()
            {
                Id = Context.Users.FirstOrDefault().Id,
                ApplicationRolesId = new List<int> { 2 }
            };

            Service.UpdateApplicationUserRole(viewModel, Context.Users.FirstOrDefault());

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
    }
}
