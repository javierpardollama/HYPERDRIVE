using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestSecurityService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestSecurityService : TestBaseService
    {
        /// <summary>
        /// Instance of <see cref="ILogger{SecurityService}"/>
        /// </summary>
        private ILogger<SecurityService> SecurityLogger;

        /// <summary>
        /// Instance of <see cref="ILogger{TokenService}"/>
        /// </summary>
        private ILogger<TokenService> TokenLogger;

        /// <summary>
        /// Instance of <see cref="TokenService"/>
        /// </summary>>
        private TokenService TokenService;

        /// <summary>
        /// Instance of <see cref="SecurityService"/>
        /// </summary>
        private SecurityService Service;


        /// <summary>
        /// Initializes a new Instance of <see cref="TestSecurityService"/>
        /// </summary>
        public TestSecurityService()
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

            TokenService = new TokenService(TokenLogger, JwtOptions);

            Service = new SecurityService(Mapper, SecurityLogger, JwtOptions, UserManager, TokenService);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Context.Users.RemoveRange(Context.Users.ToList());

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

            SecurityLogger = @loggerFactory.CreateLogger<SecurityService>();
            TokenLogger = @loggerFactory.CreateLogger<TokenService>();
        }

        /// <summary>
        /// Sets Up Context
        /// </summary>
        private void SetUpContext()
        {
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "firstuser@email.com", Email = "firstuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "seconduser@email.com", Email = "seconduser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "thirstuser@email.com", Email = "thirdtuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString(), ApplicationUserRoles = new List<ApplicationUserRole>() });

            Context.SaveChanges();
        }

        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task FindApplicationUserByEmail()
        {
            await Service.FindApplicationUserByEmail(Context.Users.FirstOrDefault().Email);

            Assert.Pass();
        }

        /// <summary>
        /// Resets Password
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task ResetPassword()
        {
            SecurityPasswordReset viewModel = new()
            {
                Email = Context.Users.FirstOrDefault(x => x.Email == "firstuser@email.com").Email,
                NewPassword = "P@55w0rd"
            };

            await Service.ResetPassword(viewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Changes Password
        /// </summary>
        [Test]
        public void ChangePassword()
        {
            SecurityPasswordChange viewModel = new()
            {
                CurrentPassword = "P@55w0rd",
                NewPassword = "P@55w0rd",
                ApplicationUser = new ViewApplicationUser
                {
                    Id = Context.Users.FirstOrDefault(x => x.Email == "seconduser@email.com").Id,
                    Email = Context.Users.FirstOrDefault(x => x.Email == "seconduser@email.com").Email
                }
            };

            Exception exception = Assert.ThrowsAsync<Exception>(async () => await Service.ChangePassword(viewModel));

            Assert.Pass();
        }

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task ChangeEmail()
        {
            SecurityEmailChange viewModel = new()
            {
                NewEmail = "newthirduser@email.com",
                ApplicationUser = new ViewApplicationUser
                {
                    Id = Context.Users.FirstOrDefault(x => x.Email == "thirdtuser@email.com").Id,
                    Email = Context.Users.FirstOrDefault(x => x.Email == "thirdtuser@email.com").Email
                }
            };

            await Service.ChangeEmail(viewModel);

            Assert.Pass();
        }
    }
}
