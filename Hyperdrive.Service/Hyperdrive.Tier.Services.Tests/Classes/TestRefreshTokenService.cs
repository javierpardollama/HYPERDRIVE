using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Helpers.Classes;
using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Microsoft.Extensions.Logging;

using NUnit.Framework;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Tests.Classes
{

    /// <summary>
    /// Represents a <see cref="TestRefreshTokenService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestRefreshTokenService : TestBaseService
    {

        /// <summary>
        /// Instance of <see cref="RefreshTokenService"/>
        /// </summary>>
        private RefreshTokenService Service;

        /// <summary>
        /// Instance of <see cref="ILogger{RefreshTokenService}"/>
        /// </summary>
        private ILogger<RefreshTokenService> Logger;

        /// <summary>
        /// Initializes a new Instance of <see cref="TestRefreshTokenService"/>
        /// </summary>
        public TestRefreshTokenService()
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

            Service = new RefreshTokenService(Context, Logger, JwtOptions, UserManager);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
           
        }

        /// <summary>
        /// Sets Up Data
        /// </summary>
        private void SetUpData()
        {
            Context.UserRefreshTokens.Add(new ApplicationUserRefreshToken
            {
                LastModified = DateTime.UtcNow,
                Deleted = false,
                Revoked = false,
                Name = Guid.NewGuid().ToString(),
                Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                ApplicationUser = new ApplicationUser
                {
                    FirstName = "First",
                    LastName = "User",
                    UserName = "firstuser@email.com",
                    Email = "firstuser@email.com",
                    PhoneNumber = int.MaxValue.ToString(),
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                }
            });

            Context.UserRefreshTokens.Add(new ApplicationUserRefreshToken
            {
                LastModified = DateTime.UtcNow,
                Deleted = false,
                Revoked = false,
                Name = Guid.NewGuid().ToString(),
                Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                ApplicationUser = new ApplicationUser
                {
                    FirstName = "Second",
                    LastName = "User",
                    UserName = "seconduser@email.com",
                    Email = "seconduser@email.com",
                    PhoneNumber = int.MaxValue.ToString(),
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                }
            });

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

            Logger = @loggerFactory.CreateLogger<RefreshTokenService>();
        }

        /// <summary>
        /// Writes Jwt Refresh Token
        /// </summary>
        [Test]
        public void WriteJwtRefreshToken()
        {
            Service.WriteJwtRefreshToken();

            Assert.Pass();
        }

        /// <summary>
        /// Generates Jwt Refresh Token Expiration Date 
        /// </summary>
        [Test]
        public void GenerateRefreshTokenExpirationDate()
        {
            Service.GenerateRefreshTokenExpirationDate();

            Assert.Pass();
        }

        /// <summary>
        /// Checks whether Jwt Refresh Token is Revoked
        /// </summary>
        [Test]
        public void IsRevoked()
        {
            SecurityRefreshTokenReset viewModel = new()
            {
                ApplicationUserId = Context.Users.FirstOrDefault().Id,
                ApplicationUserRefreshToken = StringHelper.HashString(StringHelper.GetRandomizedString())
            };

            UnauthorizedAccessException exception = Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Service.IsRevoked(viewModel));
        }

        /// <summary>
        /// Revokes Jwt Refresh Token
        /// </summary>      
        [Test]
        public async Task Revoke()
        {
            SecurityRefreshTokenReset viewModel = new()
            {
                ApplicationUserId = Context.Users.LastOrDefault().Id,
                ApplicationUserRefreshToken = Context.UserRefreshTokens.LastOrDefault().Value
            };

            await Service.Revoke(viewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Finds Application User Refresh Token By User Id
        /// </summary>
        [Test]
        public async Task FindApplicationUserRefreshTokenByApplicationUserId()
        {
            await Service.FindApplicationUserRefreshTokenByApplicationUserId(Context.Users.FirstOrDefault().Id, Context.UserRefreshTokens.FirstOrDefault().Value);

            Assert.Pass();
        }
    }
}
