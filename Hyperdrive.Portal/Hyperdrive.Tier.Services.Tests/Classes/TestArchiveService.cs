using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
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
            Context.ApplicationUser.RemoveRange(Context.ApplicationUser.ToList());

            Context.Archive.RemoveRange(Context.Archive.ToList());

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
            Context.ApplicationUser.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "firstuser@email.com", Email = "firstuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.ApplicationUser.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "seconduser@email.com", Email = "seconduser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.ApplicationUser.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "thirstuser@email.com", Email = "thirstuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });

            Context.Archive.Add(new Archive { LastModified = DateTime.Now, Deleted = false, ApplicationUserArchives = new List<ApplicationUserArchive>(), ArchiveVersions = new List<ArchiveVersion>(), Name = "firstarchive.txt" });
            Context.Archive.Add(new Archive { LastModified = DateTime.Now, Deleted = false, ApplicationUserArchives = new List<ApplicationUserArchive>(), ArchiveVersions = new List<ArchiveVersion>(), Name = "secondarchive.txt" });
            Context.Archive.Add(new Archive { LastModified = DateTime.Now, Deleted = false, ApplicationUserArchives = new List<ApplicationUserArchive>(), ArchiveVersions = new List<ArchiveVersion>(), Name = "thirdarchive.txt" });

            Context.SaveChanges();
        }

        /// <summary>
        /// Finds Archive By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
       [Test]
        public async Task FindArchiveById()
        {
            await Service.FindArchiveById(Context.Archive.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Removes Archive By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task RemoveArchiveById()
        {
            await Service.RemoveArchiveById(Context.Archive.FirstOrDefault().Id);

            Assert.Pass();
        }

        [Test]
        public async Task FindAllArchive()
        {
            await Service.FindAllArchive();

            Assert.Pass();
        }

        [Test]
        public async Task FindAllArchiveByApplicationUserId()
        {
            await Service.FindAllArchiveByApplicationUserId(Context.ApplicationUser.FirstOrDefault().Id);

            Assert.Pass();
        }

        [Test]
        public async Task FindAllSharedArchiveByApplicationUserId()
        {
            await Service.FindAllSharedArchiveByApplicationUserId(Context.ApplicationUser.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindApplicationUserById()
        {
            await Service.FindApplicationUserById(Context.ApplicationUser.FirstOrDefault().Id);

            Assert.Pass();
        }

        [Test]
        public async Task FindAllArchiveVersionByArchiveId()
        {
            await Service.FindAllArchiveVersionByArchiveId(Context.Archive.FirstOrDefault().Id);

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindApplicationUserByEmail()
        {
            await Service.FindApplicationUserByEmail(Context.ApplicationUser.FirstOrDefault().Email);

            Assert.Pass();
        }

        [Test]
        public async Task AddArchive()
        {
            AddArchive @addArchive = new()
            {
                ApplicationUsersId = Context.ApplicationUser.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = "foutharchive.txt",
                Size = 1024,
                Type = "Text",
                By = new ViewApplicationUser()
                {
                    Id = Context.ApplicationUser.FirstOrDefault().Id,
                    Email = Context.ApplicationUser.FirstOrDefault().Email
                }
            };

            await Service.AddArchive(@addArchive);

            Assert.Pass();
        }

        [Test]
        public void AddApplicationUserArchive() 
        {
            AddArchive @addArchive = new()
            {
                ApplicationUsersId = Context.ApplicationUser.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = "fiftharchive.txt",
                Size = 1024,
                Type = "Text",
                By = new ViewApplicationUser()
                {
                    Id = Context.ApplicationUser.FirstOrDefault().Id,
                    Email = Context.ApplicationUser.FirstOrDefault().Email
                }
            };

            Service.AddApplicationUserArchive(@addArchive, Context.Archive.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void AddArchiveVersion() 
        {
            AddArchive @addArchive = new()
            {
                ApplicationUsersId = Context.ApplicationUser.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = "sixtharchive.txt",
                Size = 1024,
                Type = "Text",
                By = new ViewApplicationUser()
                {
                    Id = Context.ApplicationUser.FirstOrDefault().Id,
                    Email = Context.ApplicationUser.FirstOrDefault().Email
                }
            };

            Service.AddArchiveVersion(@addArchive, Context.Archive.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public async Task UpdateArchive()
        {
            UpdateArchive @updateArchive = new()
            {
                Id = Context.Archive.FirstOrDefault().Id,
                ApplicationUsersId = Context.ApplicationUser.ToList().Select(x=>x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archive.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                By = new ViewApplicationUser() 
                {
                    Id = Context.ApplicationUser.FirstOrDefault().Id,
                    Email = Context.ApplicationUser.FirstOrDefault().Email
                }
            };

            await Service.UpdateArchive(@updateArchive);

            Assert.Pass();
        }

        [Test]
        public void UpdateApplicationUserArchive()
        {
            UpdateArchive @updateArchive = new()
            {
                Id = Context.Archive.FirstOrDefault().Id,
                ApplicationUsersId = Context.ApplicationUser.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archive.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                By = new ViewApplicationUser()
                {
                    Id = Context.ApplicationUser.FirstOrDefault().Id,
                    Email = Context.ApplicationUser.FirstOrDefault().Email
                }
            };

            Service.UpdateApplicationUserArchive(@updateArchive, Context.Archive.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void UpdateArchiveVersion()
        {
            UpdateArchive @updateArchive = new()
            {
                Id = Context.Archive.FirstOrDefault().Id,
                ApplicationUsersId = Context.ApplicationUser.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archive.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                By = new ViewApplicationUser()
                {
                    Id = Context.ApplicationUser.FirstOrDefault().Id,
                    Email = Context.ApplicationUser.FirstOrDefault().Email
                }
            };

            Service.UpdateArchiveVersion(@updateArchive, Context.Archive.FirstOrDefault());

            Assert.Pass();
        }

        [Test]
        public void CheckName()
        {
            UpdateArchive @updateArchive = new()
            {
                Id = Context.Archive.FirstOrDefault().Id,
                ApplicationUsersId = Context.ApplicationUser.ToList().Select(x => x.Id).ToList(),
                Data = new byte[10],
                Name = Context.Archive.FirstOrDefault().Name,
                Size = 1024,
                Type = "Text",
                By = new ViewApplicationUser()
                {
                    Id = Context.ApplicationUser.FirstOrDefault().Id,
                    Email = Context.ApplicationUser.FirstOrDefault().Email
                }
            };

            Exception exception = Assert.ThrowsAsync<Exception>(async () => await Service.CheckName(@updateArchive));

            Assert.Pass();
        }
    }
}
