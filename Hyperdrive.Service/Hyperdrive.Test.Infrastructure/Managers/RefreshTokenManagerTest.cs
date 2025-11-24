using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Managers;
using Hyperdrive.Test.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="RefreshTokenManagerTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class RefreshTokenManagerTest: BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{RefreshTokenManager}"/>
    /// </summary>
    private ILogger<RefreshTokenManager> Logger;

    /// <summary>
    /// Instance of <see cref="RefreshTokenManager"/>
    /// </summary>
    private RefreshTokenManager Manager;

    /// <summary>
    /// Sets Up
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        InstallServices();

        Context = new ApplicationContext(ContextOptionsBuilder.Options);
        Context.Seed();

        InstallLogger();      

        Manager = new RefreshTokenManager(Context, Logger, ApiOptions);
    }

    /// <summary>
    /// Installs Logger
    /// </summary>
    private void InstallLogger()
    {
        ILoggerFactory @loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole();
        });

        Logger = @loggerFactory.CreateLogger<RefreshTokenManager>();
    }    

    /// <summary>
    /// Writes Jwt Refresh Token
    /// </summary>
    [Test]
    public void WriteJwtRefreshToken()
    {
        Manager.WriteJwtRefreshToken();
        Assert.Pass();
    }

    /// <summary>
    /// Generates Jwt Refresh Token Expiration Date 
    /// </summary>
    [Test]
    public void GenerateRefreshTokenExpirationDate()
    {
        Manager.GenerateRefreshTokenExpirationDate();
        Assert.Pass();
    }

    /// <summary>
    /// Checks whether Jwt Refresh Token is Revoked
    /// </summary>
    [Test]
    public async Task IsRevoked()
    {
        await Manager.IsRevoked(10, "i5E%@VRMZ)%3AuWuA+A+%PAcEE0q.x");
        Assert.Pass();
    }

    /// <summary>
    /// Revokes Jwt Refresh Token
    /// </summary>    
    [Test]
    public async Task Revoke()
    {
        await Manager.Revoke(10, "i5E%@VRMZ)%3AuWuA+A+%PAcEE0q.x");
        Assert.Pass();
    }

    /// <summary>
    /// Finds Application User Refresh Token By User Id
    /// </summary>
    [Test]
    public async Task FindApplicationUserRefreshTokenByCredentials()
    {
        await Manager.FindApplicationUserRefreshTokenByCredentials(10, "i5E%@VRMZ)%3AuWuA+A+%PAcEE0q.x");
        Assert.Pass();
    }

    /// <summary>
    /// Adds Application User Refresh Token
    /// </summary>
    [Test]
    public async Task AddApplicationUserRefreshToken()
    {
        var @user = new ApplicationUser
        {
            Id = 12,
            FirstName = "Neva",
            LastName = "Shelly",
            UserName = "neva.shelly",
            Email = "neva.shelly@email.com",
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };
        await Manager.AddApplicationUserRefreshToken(@user);
        
        Assert.Pass();
    }
}