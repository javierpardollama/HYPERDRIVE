using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Exceptions.Exceptions;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestDriveItemService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestDriveItemService : TestBaseService
    {
        /// <summary>
        /// Instance of <see cref="ILogger{DriveItemService}"/>
        /// </summary>
        private ILogger<DriveItemService> Logger;

        /// <summary>
        /// Instance of <see cref="DriveItemService"/>
        /// </summary>>
        private DriveItemService Service;

        /// <summary>
        /// Initializes a new Instance of <see cref="TestDriveItemService"/>
        /// </summary>
        public TestDriveItemService()
        {

        }

        /// <summary>
        /// Sets Up
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetUpServices();

            SetUpMapper();

            SetUpLogger();

            SetUpData();

            Service = new DriveItemService(UserManager, Context, Mapper, Logger);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
          
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

            Logger = @loggerFactory.CreateLogger<DriveItemService>();
        }

        /// <summary>
        /// Sets Up Data
        /// </summary>
        private void SetUpData()
        {
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "firstuser@email.com", Email = "firstuser@email.com", LastModified = DateTime.UtcNow, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "seconduser@email.com", Email = "seconduser@email.com", LastModified = DateTime.UtcNow, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "thirstuser@email.com", Email = "thirstuser@email.com", LastModified = DateTime.UtcNow, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString() });

            Context.DriveItems.Add(new DriveItem { LastModified = DateTime.UtcNow, Deleted = false, DriveItemVersions = new List<DriveItemVersion>(), Name = "firstarchive.txt"  });
            Context.DriveItems.Add(new DriveItem { LastModified = DateTime.UtcNow, Deleted = false, DriveItemVersions = new List<DriveItemVersion>(), Name = "secondarchive.txt" });
            Context.DriveItems.Add(new DriveItem { LastModified = DateTime.UtcNow, Deleted = false, DriveItemVersions = new List<DriveItemVersion>(), Name = "thirdarchive.txt" });

            Context.SaveChanges();
        }

        /// <summary>
        /// Finds DriveItem By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindDriveItemById()
        {
            await Service.FindDriveItemById(Context.DriveItems.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Removes DriveItem By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task RemoveDriveItemById()
        {
            await Service.RemoveDriveItemById(Context.DriveItems.FirstOrDefault().Id);

            Assert.Pass();
        }

        [Test]
        public async Task FindAllDriveItem()
        {
            await Service.FindAllDriveItem();

            Assert.Pass();
        }

        [Test]
        public async Task FindPaginatedDriveItemByApplicationUserId()
        {
            await Service.FindPaginatedDriveItemByApplicationUserId(new FilterPageDriveItem { Index = 1, Size = 5, ApplicationUserId = Context.Users.FirstOrDefault().Id });

            Assert.Pass();
        }

        [Test]
        public async Task FindPaginatedSharedDriveItemByApplicationUserId()
        {
            await Service.FindPaginatedSharedDriveItemByApplicationUserId(new FilterPageDriveItem { Index = 1, Size = 5, ApplicationUserId = Context.Users.FirstOrDefault().Id });

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

        [Test]
        public async Task FindAllDriveItemVersionByDriveItemId()
        {
            await Service.FindAllDriveItemVersionByDriveItemId(Context.DriveItems.FirstOrDefault().Id);

            Assert.Pass();
        }

        [Test]
        public async Task AddDriveItem()
        {
            AddDriveItem @addDriveItem = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10].ToString(),
                Name = "foutharchive.txt",
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id,
            };

            await Service.AddDriveItem(@addDriveItem);

            Assert.Pass();
        }

        [Test]
        public void AddApplicationUserDriveItem()
        {
            AddDriveItem @addDriveItem = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10].ToString(),
                Name = "fiftharchive.txt",
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id,
            };

            Service.AddApplicationUserDriveItem(@addDriveItem, Context.DriveItems.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void AddDriveItemVersion()
        {
            AddDriveItem @addDriveItem = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10].ToString(),
                Name = "sixtharchive.txt",
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id,
            };

            Service.AddDriveItemVersion(@addDriveItem, Context.DriveItems.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public async Task UpdateDriveItem()
        {
            UpdateDriveItem @updateDriveItem = new()
            {
                Id = Context.DriveItems.FirstOrDefault().Id,
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10].ToString(),
                Name = Context.DriveItems.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id
            };

            await Service.UpdateDriveItem(@updateDriveItem);

            Assert.Pass();
        }

        [Test]
        public void UpdateApplicationUserDriveItem()
        {
            UpdateDriveItem @updateDriveItem = new()
            {
                Id = Context.DriveItems.FirstOrDefault().Id,
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10].ToString(),
                Name = Context.DriveItems.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id
            };

            Service.UpdateApplicationUserDriveItem(@updateDriveItem, Context.DriveItems.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void UpdateDriveItemVersion()
        {
            UpdateDriveItem @updateDriveItem = new()
            {
                Id = Context.DriveItems.FirstOrDefault().Id,
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10].ToString(),
                Name = Context.DriveItems.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id
            };

            Service.UpdateDriveItemVersion(@updateDriveItem, Context.DriveItems.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void CheckName()
        {
            AddDriveItem @addDriveItem = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10].ToString(),
                Name = Context.DriveItems.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id
            };

            ServiceException exception = Assert.ThrowsAsync<ServiceException>(async () => await Service.CheckName(@addDriveItem));

            Assert.Pass();
        }
    }
}
