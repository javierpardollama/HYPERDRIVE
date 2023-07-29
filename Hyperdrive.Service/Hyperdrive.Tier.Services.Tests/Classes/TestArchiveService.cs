using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using Microsoft.Extensions.Logging;

using NUnit.Framework;


namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestAuthService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestArchiveService : TestBaseService
    {
        /// <summary>
        /// Instance of <see cref="ILogger{ArchiveService}"/>
        /// </summary>
        private ILogger<ArchiveService> Logger;

        /// <summary>
        /// Instance of <see cref="ArchiveService"/>
        /// </summary>>
        private ArchiveService Service;

        /// <summary>
        /// Initializes a new Instance of <see cref="TestArchiveService"/>
        /// </summary>
        public TestArchiveService()
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

            Service = new ArchiveService(UserManager, Context, Mapper, Logger);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Context.Users.RemoveRange(Context.Users.ToList());

            Context.Archives.RemoveRange(Context.Archives.ToList());

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

            Logger = @loggerFactory.CreateLogger<ArchiveService>();
        }

        /// <summary>
        /// Sets Up Context
        /// </summary>
        private void SetUpContext()
        {
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "firstuser@email.com", Email = "firstuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "seconduser@email.com", Email = "seconduser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "thirstuser@email.com", Email = "thirstuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });

            Context.Archives.Add(new Archive { LastModified = DateTime.Now, Deleted = false, ApplicationUserArchives = new List<ApplicationUserArchive>(), ArchiveVersions = new List<ArchiveVersion>(), Name = "firstarchive.txt" });
            Context.Archives.Add(new Archive { LastModified = DateTime.Now, Deleted = false, ApplicationUserArchives = new List<ApplicationUserArchive>(), ArchiveVersions = new List<ArchiveVersion>(), Name = "secondarchive.txt" });
            Context.Archives.Add(new Archive { LastModified = DateTime.Now, Deleted = false, ApplicationUserArchives = new List<ApplicationUserArchive>(), ArchiveVersions = new List<ArchiveVersion>(), Name = "thirdarchive.txt" });

            Context.SaveChanges();
        }

        /// <summary>
        /// Finds Archive By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
       [Test]
        public async Task FindArchiveById()
        {
            await Service.FindArchiveById(Context.Archives.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Removes Archive By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task RemoveArchiveById()
        {
            await Service.RemoveArchiveById(Context.Archives.FirstOrDefault().Id);

            Assert.Pass();
        }

        [Test]
        public async Task FindAllArchive()
        {
            await Service.FindAllArchive();

            Assert.Pass();
        }

        [Test]
        public async Task FindPaginatedArchiveByApplicationUserId()
        {
            await Service.FindPaginatedArchiveByApplicationUserId(new FilterPageArchive { Index = 1, Size = 5, ApplicationUserId = Context.Users.FirstOrDefault().Id });

            Assert.Pass();
        }

        [Test]
        public async Task FindPaginatedSharedArchiveByApplicationUserId()
        {
            await Service.FindPaginatedSharedArchiveByApplicationUserId(new FilterPageArchive { Index = 1, Size = 5, ApplicationUserId = Context.Users.FirstOrDefault().Id });

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
        public async Task FindAllArchiveVersionByArchiveId()
        {
            await Service.FindAllArchiveVersionByArchiveId(Context.Archives.FirstOrDefault().Id);

            Assert.Pass();
        }      

        [Test]
        public async Task AddArchive()
        {
            AddArchive @addArchive = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = "foutharchive.txt",
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id,                   
            };

            await Service.AddArchive(@addArchive);

            Assert.Pass();
        }

        [Test]
        public void AddApplicationUserArchive() 
        {
            AddArchive @addArchive = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = "fiftharchive.txt",
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id,
            };

            Service.AddApplicationUserArchive(@addArchive, Context.Archives.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void AddArchiveVersion() 
        {
            AddArchive @addArchive = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = "sixtharchive.txt",
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id,
            };

            Service.AddArchiveVersion(@addArchive, Context.Archives.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public async Task UpdateArchive()
        {
            UpdateArchive @updateArchive = new()
            {
                Id = Context.Archives.FirstOrDefault().Id,
                ApplicationUsersId = Context.Users.ToList().Select(x=>x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archives.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id                   
            };

            await Service.UpdateArchive(@updateArchive);

            Assert.Pass();
        }

        [Test]
        public void UpdateApplicationUserArchive()
        {
            UpdateArchive @updateArchive = new()
            {
                Id = Context.Archives.FirstOrDefault().Id,
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archives.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id
            };

            Service.UpdateApplicationUserArchive(@updateArchive, Context.Archives.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void UpdateArchiveVersion()
        {
            UpdateArchive @updateArchive = new()
            {
                Id = Context.Archives.FirstOrDefault().Id,
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archives.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id
            };

            Service.UpdateArchiveVersion(@updateArchive, Context.Archives.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void CheckName()
        {
            AddArchive @addArchive = new()
            {
                ApplicationUsersId = Context.Users.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archives.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                ApplicationUserId = Context.Users.FirstOrDefault().Id
            };

            Exception exception = Assert.ThrowsAsync<Exception>(async () => await Service.CheckName(@addArchive));

            Assert.Pass();
        }
    }
}
