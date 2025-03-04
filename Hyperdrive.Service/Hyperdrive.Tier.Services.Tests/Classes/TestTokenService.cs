using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;

using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

using System;
using System.Linq;

namespace Hyperdrive.Tier.Services.Tests.Classes
{
    /// <summary>
    /// Represents a <see cref="TestTokenService"/> class. Inherits <see cref="TestBaseService"/>
    /// </summary>
    [TestFixture]
    public class TestTokenService : TestBaseService
    {
        private TokenService Service;


        /// <summary>
        /// Instance of <see cref="ILogger{TokenService}"/>
        /// </summary>
        private ILogger<TokenService> Logger;

        /// <summary>
        /// Initializes a new Instance of <see cref="TestTokenService"/>
        /// </summary>
        public TestTokenService()
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

            Service = new TokenService(Logger, JwtOptions);
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

            Logger = @loggerFactory.CreateLogger<TokenService>();
        }

        /// <summary>
        /// Generates Token Descriptor
        /// </summary>
        [Test]
        public void GenerateTokenDescriptor()
        {
            Service.GenerateTokenDescriptor(Context.Users.FirstOrDefault());

            Assert.Pass();
        }

        /// <summary>
        /// Creates Token
        /// </summary>
        [Test]
        public void CreateToken()
        {
            SecurityTokenDescriptor @tokenDescriptor = new();

            Service.CreateToken(@tokenDescriptor);

            Assert.Pass();
        }       

        /// <summary>
        /// Generates Symmetric Security Key
        /// </summary>
        [Test]
        public void GenerateSymmetricSecurityKey()
        {
            Service.GenerateSymmetricSecurityKey();

            Assert.Pass();
        }

        /// <summary>
        /// Generates Signing Credentials
        /// </summary>
        [Test]
        public void GenerateSigningCredentials()
        {
            Service.GenerateSigningCredentials(Service.GenerateSymmetricSecurityKey());

            Assert.Pass();
        }

        /// <summary>
        /// Generates Token Expiration Date
        /// </summary>
        [Test]
        public void GenerateTokenExpirationDate()
        {
            Service.GenerateTokenExpirationDate();

            Assert.Pass();
        }

        /// <summary>
        /// Generates Jwt Claims
        /// </summary>
        [Test]
        public void GenerateJwtClaims()
        {
            Service.GenerateJwtClaims(Context.Users.FirstOrDefault());

            Assert.Pass();
        }
    }
}
