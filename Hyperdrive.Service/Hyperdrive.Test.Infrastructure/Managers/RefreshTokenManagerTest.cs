using System;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Managers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

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

        InstallLogger();

        Seed();

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
    /// Seeds
    /// </summary>
    private void Seed()
    {
        if (!Context.Roles.Any())
        {
            Context.Roles.Add(new ApplicationRole
            {
                Id = 1,
                Name = "Dungeon Master",
                NormalizedName = "DUNGEON_MASTER",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ImageUri = "URL/Dungeon_Master_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 2,
                Name = "Paladin",
                NormalizedName = "PALADIN",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ImageUri = "URL/Paladin_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 3,
                Name = "Sorceress",
                NormalizedName = "SORCERESS",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ImageUri = "URL/Sorceress_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 4,
                Name = "Rogue",
                NormalizedName = "ROGUE",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ImageUri = "URL/Rogue_2_500px.png"
            });
            Context.Roles.Add(new ApplicationRole
            {
                Id = 5,
                Name = "Bard",
                NormalizedName = "BARD",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ImageUri = "URL/Bard_500px.png"
            });
        }

        if (!Context.Users.Any())
        {
            Context.Users.Add(new ApplicationUser
            {
                Id = 1,
                FirstName = "Stafford",
                LastName = "Parker",
                UserName = "stafford.parker",
                Email = "stafford.parker@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
            Context.Users.Add(new ApplicationUser
            {
                Id = 2,
                FirstName = "Dee",
                LastName = "Sandy",
                UserName = "dee.sandy",
                Email = "dee.sandy@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
            Context.Users.Add(new ApplicationUser
            {
                Id = 3,
                FirstName = "Orinda Navy",
                LastName = "Navy",
                UserName = "orinda.navy",
                Email = "orinda.navy@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
            Context.Users.Add(new ApplicationUser
            {
                Id = 4,
                FirstName = "Genesis",
                LastName = "Gavin",
                UserName = "genesis.gavin",
                Email = "genesis.gavin@email.com",
                LastModified = DateTime.UtcNow,
                Deleted = false,
                SecurityStamp = new Guid().ToString()
            });
        }

        if (!Context.UserRefreshTokens.Any())
        {
            Context.UserRefreshTokens.Add(new ApplicationUserRefreshToken
            {
                Id = 1,
                Value = "i5E%@VRMZ)%3AuWuA+A+%PAcEE0q.x",
                Revoked = false,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ApplicationUser = new ApplicationUser
                {
                    Id = 10,
                    FirstName = "Cali ",
                    LastName = "Trafford",
                    UserName = "cali.trafford",
                    Email = "cali.trafford@email.com",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                }
            });
            Context.UserRefreshTokens.Add(new ApplicationUserRefreshToken
            {
                Id = 2,
                Value = "&91eVg+82z*q5qfwCLp.*f=x)];]27",
                Revoked = false,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                LastModified = DateTime.UtcNow,
                Deleted = false,
                ApplicationUser = new ApplicationUser
                {
                    Id = 11,
                    FirstName = "Barb Román",
                    LastName = "Román",
                    UserName = "barb.román",
                    Email = "barb.román@email.com",
                    LastModified = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                }
            });
        }

        Context.SaveChanges();
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
            LastModified = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };
        await Manager.AddApplicationUserRefreshToken(@user);
        
        Assert.Pass();
    }
}