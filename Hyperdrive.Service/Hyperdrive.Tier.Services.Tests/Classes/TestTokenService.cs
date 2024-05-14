﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using Hyperdrive.Tier.Contexts.Classes;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Classes;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

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
            SetUpContextOptions();

            SetUpJwtOptions();

            SetUpServices();

            SetUpMapper();

            SetUpLogger();

            SetUpContext(Context);

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
        /// Sets Up Context
        /// </summary>
        /// <param name="context">Injected <see cref="ApplicationContext"/></param>
        private static void SetUpContext(ApplicationContext @context)
        {
            @context.Users.Add(new ApplicationUser { FirstName = "First", LastName = "User", UserName = "firstuser@email.com", Email = "firstuser@email.com", PhoneNumber = int.MaxValue.ToString(), LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString() });
            @context.Users.Add(new ApplicationUser { FirstName = "Second", LastName = "User", UserName = "seconduser@email.com", Email = "seconduser@email.com", PhoneNumber = int.MaxValue.ToString(), LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString() });
            @context.Users.Add(new ApplicationUser { FirstName = "Thirst", LastName = "User", UserName = "thirstuser@email.com", Email = "thirstuser@email.com", PhoneNumber = int.MaxValue.ToString(), LastModified = DateTime.Now, Deleted = false, SecurityStamp = new Guid().ToString() });

            @context.SaveChanges();
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
        /// Generates Jwt Token
        /// </summary>
        [Test]
        public void GenerateJwtToken()
        {
            Service.GenerateJwtToken(Context.Users.FirstOrDefault());

            Assert.Pass();
        }

        /// <summary>
        /// Writes Jwt Token
        /// </summary>
        [Test]
        public void WriteJwtToken()
        {
            JwtSecurityToken JwtSecurityToken = new();

            Service.WriteJwtToken(JwtSecurityToken);

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
