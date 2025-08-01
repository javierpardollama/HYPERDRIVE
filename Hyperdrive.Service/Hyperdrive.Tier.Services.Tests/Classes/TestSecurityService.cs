﻿using Hyperdrive.Tier.Entities.Classes;
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
        /// Instance of <see cref="ILogger{RefreshTokenService}"/>
        /// </summary>
        private ILogger<RefreshTokenService> RefreshTokenLogger;
        
        /// <summary>
        /// Instance of <see cref="ILogger{TokenService}"/>
        /// </summary>
        private ILogger<TokenService> TokenLogger;

        /// <summary>
        /// Instance of <see cref="TokenService"/>
        /// </summary>>
        private TokenService TokenService;

        /// <summary>
        /// Instance of <see cref="RefreshTokenService"/>
        /// </summary>>
        private RefreshTokenService RefreshTokenService;

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
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetUpServices();

            SetUpMapper();

            SetUpLogger();

            SetUpData();

            TokenService = new TokenService(Context, TokenLogger, JwtOptions, UserManager);

            RefreshTokenService = new RefreshTokenService(Context, RefreshTokenLogger, JwtOptions, UserManager);

            Service = new SecurityService(Context, Mapper, SecurityLogger, JwtOptions, UserManager, TokenService, RefreshTokenService);
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

            SecurityLogger = @loggerFactory.CreateLogger<SecurityService>();
            RefreshTokenLogger = loggerFactory.CreateLogger<RefreshTokenService>();
            TokenLogger = loggerFactory.CreateLogger<TokenService>();
        }

        /// <summary>
        /// Sets Up Data
        /// </summary>
        private void SetUpData()
        {
            Context.Users.Add(new ApplicationUser
            {
                PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb",
                UserName = "firstuser@email.com",
                Email = "firstuser@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ConcurrencyStamp = new Guid().ToString(),
                SecurityStamp = new Guid().ToString(),
                ApplicationUserRefreshTokens = [new ApplicationUserRefreshToken
                {
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    Revoked = false,
                    Name = Guid.NewGuid().ToString(),
                    Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                    ExpiresAt = DateTime.UtcNow.AddDays(2)
                }]
            });
            Context.Users.Add(new ApplicationUser
            {
                PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb",
                UserName = "seconduser@email.com",
                Email = "seconduser@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ConcurrencyStamp = new Guid().ToString(),
                SecurityStamp = new Guid().ToString(),
                ApplicationUserRefreshTokens = [new ApplicationUserRefreshToken
                {
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    Revoked = false,
                    Name = Guid.NewGuid().ToString(),
                    Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                    ExpiresAt = DateTime.UtcNow.AddDays(2)
                }]
            });
            Context.Users.Add(new ApplicationUser
            {
                PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb",
                UserName = "thirstuser@email.com",
                Email = "thirduser@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ConcurrencyStamp = new Guid().ToString(),
                SecurityStamp = new Guid().ToString(),
                ApplicationUserRefreshTokens = [new ApplicationUserRefreshToken
                {
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    Revoked = false,
                    Name = Guid.NewGuid().ToString(),
                    Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                    ExpiresAt = DateTime.UtcNow.AddDays(2)
                }]
            });
            Context.Users.Add(new ApplicationUser
            {
                PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb",
                UserName = "fourthuser@email.com",
                Email = "fourthuser@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ConcurrencyStamp = new Guid().ToString(),
                SecurityStamp = new Guid().ToString(),
                ApplicationUserRefreshTokens = [new ApplicationUserRefreshToken
                {
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    Revoked = false,
                    Name = Guid.NewGuid().ToString(),
                    Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                    ExpiresAt = DateTime.UtcNow.AddDays(2)
                }]
            });
            Context.Users.Add(new ApplicationUser
            {
                PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb",
                UserName = "fifthuser@email.com",
                Email = "fifthuser@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ConcurrencyStamp = new Guid().ToString(),
                SecurityStamp = new Guid().ToString(),
                ApplicationUserRefreshTokens = [new ApplicationUserRefreshToken
                {
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    Revoked = false,
                    Name = Guid.NewGuid().ToString(),
                    Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                    ExpiresAt = DateTime.UtcNow.AddDays(2)
                }]
            });
            Context.Users.Add(new ApplicationUser
            {
                PasswordHash = "dcb97c304778b75e4309bdd51d61c906dc184cd37df1256fdafd3e54cf6218bb",
                UserName = "sixthuser@email.com",
                Email = "sixthuser@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ConcurrencyStamp = new Guid().ToString(),
                SecurityStamp = new Guid().ToString(),
                ApplicationUserRefreshTokens = [new ApplicationUserRefreshToken
                {
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    Revoked = false,
                    Name = Guid.NewGuid().ToString(),
                    Value = StringHelper.HashString(StringHelper.GetRandomizedString()),
                    ExpiresAt = DateTime.UtcNow.AddDays(2)
                }]
            });

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
        /// Resets Password
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task ResetPassword()
        {
            SecurityPasswordReset viewModel = new()
            {
                Email = Context.Users.FirstOrDefault(x => x.Email == "seconduser@email.com").Email,
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
                ApplicationUserId = Context.Users.FirstOrDefault(x => x.Email == "thirduser@email.com").Id
            };

            UnauthorizedAccessException exception = Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await Service.ChangePassword(viewModel));

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
                ApplicationUserId = Context.Users.FirstOrDefault(x => x.Email == "fourthuser@email.com").Id
            };

            await Service.ChangeEmail(viewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Changes Phone Number
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task ChangePhoneNumber()
        {
            SecurityPhoneNumberChange viewModel = new()
            {
                NewPhoneNumber = "654321987",
                ApplicationUserId = Context.Users.FirstOrDefault(x => x.Email == "fifthuser@email.com").Id
            };

            await Service.ChangePhoneNumber(viewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <returns>Instance of <see cref="Task"/></returns>
        [Test]
        public async Task ChangeName()
        {
            SecurityNameChange viewModel = new()
            {
                NewFirstName = "New Fifth User FirstName",
                NewLastName = "New Fifth User LastName",
                ApplicationUserId = Context.Users.FirstOrDefault(x => x.Email == "sixthuser@email.com").Id
            };

            await Service.ChangeName(viewModel);

            Assert.Pass();
        }

        /// <summary>
        /// Refreshes Tokens
        /// </summary>
        [Test]
        public async Task RefreshTokens()
        {
            SecurityRefreshTokenReset viewModel = new()
            {
                ApplicationUserId = Context.Users.LastOrDefault().Id,
                ApplicationUserRefreshToken = Context.UserRefreshTokens.LastOrDefault().Value
            };

            await Service.RefreshTokens(viewModel);

            Assert.Pass();
        }
    }
}
