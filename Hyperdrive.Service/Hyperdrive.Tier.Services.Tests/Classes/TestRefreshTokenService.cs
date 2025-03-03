using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

using System;
using System.Linq;

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

            Service = new RefreshTokenService(Logger, JwtOptions);
        }

        /// <summary>
        /// Tears Down
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Context.Users.RemoveRange(Context.Users.ToList());
        }

        /// <summary>
        /// Sets Up Data
        /// </summary>
        private void SetUpData()
        {
            Context.Users.Add(new ApplicationUser { FirstName = "First", LastName = "User", UserName = "firstuser@email.com", Email = "firstuser@email.com", PhoneNumber = int.MaxValue.ToString(), LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString() });
            Context.Users.Add(new ApplicationUser { FirstName = "Second", LastName = "User", UserName = "seconduser@email.com", Email = "seconduser@email.com", PhoneNumber = int.MaxValue.ToString(), LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString() });
            Context.Users.Add(new ApplicationUser { FirstName = "Thirst", LastName = "User", UserName = "thirstuser@email.com", Email = "thirstuser@email.com", PhoneNumber = int.MaxValue.ToString(), LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString() });

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

    }
}
