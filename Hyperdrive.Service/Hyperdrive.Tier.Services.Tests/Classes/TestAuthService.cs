using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Auth;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestAuthService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestAuthService : TestBaseService
    {
        /// <summary>
        /// Instance of <see cref="ILogger{AuthService}"/>
        /// </summary>
        private ILogger<AuthService> AuthLogger;

        /// <summary>
        /// Instance of <see cref="ILogger{TokenService}"/>
        /// </summary>
        private ILogger<TokenService> TokenLogger;


        /// <summary>
        /// Instance of <see cref="TokenService"/>
        /// </summary>>
        private TokenService TokenService;

        /// <summary>
        /// Instance of <see cref="AuthService"/>
        /// </summary>
        private AuthService Service;


        /// <summary>
        /// Initializes a new Instance of <see cref="TestAuthService"/>
        /// </summary>
        public TestAuthService()
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

            SetUpHttpContext();

            SetUpMapper();

            SetUpLogger();

            SetUpContext();

            TokenService = new TokenService(TokenLogger, JwtOptions);

            Service = new AuthService(Mapper, AuthLogger, JwtOptions, UserManager, SignInManager, TokenService);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
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

            AuthLogger = @loggerFactory.CreateLogger<AuthService>();
            TokenLogger = @loggerFactory.CreateLogger<TokenService>();
        }

        /// <summary>
        /// Sets Up Context
        /// </summary>
        private void SetUpContext()
        {
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "firstuser@email.com", Email = "firstuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "seconduser@email.com", Email = "seconduser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString() });
            Context.Users.Add(new ApplicationUser { PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb", UserName = "thirstuser@email.com", Email = "thirstuser@email.com", LastModified = DateTime.Now, Deleted = false, ConcurrencyStamp = new Guid().ToString(), SecurityStamp = new Guid().ToString() });

            Context.SaveChanges();
        }

        /// <summary>
        /// Signs In
        /// </summary>
        [Test]
        public void SignIn()
        {
            AuthSignIn viewModel = new()
            {
                Email = "firstuser@email.com",
                Password = "P@55w0rd"
            };

            Exception exception = Assert.ThrowsAsync<Exception>(async () => await Service.SignIn(viewModel));

            Assert.Pass();
        }

        /// <summary>
        /// Joins In
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task JoinIn()
        {
            AuthJoinIn viewModel = new()
            {
                Email = "fifthuser@email.com",
                Password = "P@55w0rd"
            };

            await Service.JoinIn(viewModel);

            Assert.Pass();
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
        /// Checks Email
        /// </summary>
        [Test]
        public void CheckEmail()
        {
            AuthJoinIn viewModel = new()
            {
                Email = "firstuser@email.com",
                Password = "P@55w0rd"
            };

            Exception exception = Assert.ThrowsAsync<Exception>(async () => await Service.CheckEmail(viewModel));

            Assert.Pass();
        }
    }
}
