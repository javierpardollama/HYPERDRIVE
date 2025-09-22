using System;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Managers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="TokenManagertTest"/> class. Inherits <see cref="BaseManagerTest"/>
/// </summary>
[TestFixture]
public class TokenManagertTest : BaseManagerTest
{
    /// <summary>
    /// Instance of <see cref="ILogger{TokenManager}"/>
    /// </summary>
    private ILogger<TokenManager> Logger;

    /// <summary>
    /// Instance of <see cref="TokenManager"/>
    /// </summary>
    private TokenManager Manager;

    /// <summary>
    /// Sets Up
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        InstallServices();

        InstallLogger();

        Seed();

        Manager = new TokenManager(Context, Logger, ApiOptions);
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

        Logger = @loggerFactory.CreateLogger<TokenManager>();
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

        Context.SaveChanges();
    }

    /// <summary>
    /// Generates Token Descriptor
    /// </summary>
    [Test]
    public void GenerateTokenDescriptor()
    {
        var @user = new ApplicationUser
        {
            Id = 5,
            FirstName = "Benita",
            LastName = "Janessa",
            UserName = "benita.janessa",
            Email = "benita.janessa@email.com",
            LastModified = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };

        Manager.GenerateTokenDescriptor(user);

        Assert.Pass();
    }

    /// <summary>
    /// Creates Token
    /// </summary>
    [Test]
    public void CreateToken()
    {
        var @user = new ApplicationUser
        {
            Id = 6,
            FirstName = "sapphire",
            LastName = "Tadeo",
            UserName = "sapphire.tadeo",
            Email = "sapphire.tadeo@email.com",
            LastModified = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };

        var @descriptor = Manager.GenerateTokenDescriptor(@user);

        Manager.CreateToken(@descriptor);

        Assert.Pass();
    }

    /// <summary>
    /// Generates Symmetric Security Key
    /// </summary>
    [Test]
    public void GenerateSymmetricSecurityKey()
    {
        Manager.GenerateSymmetricSecurityKey();

        Assert.Pass();
    }

    /// <summary>
    /// Generates Signing Credentials
    /// </summary>
    public void GenerateSigningCredentials()
    {
        var @key = Manager.GenerateSymmetricSecurityKey();

        Manager.GenerateSigningCredentials(@key);

        Assert.Pass();
    }

    /// <summary>
    /// Generates Token Expiration Date 
    /// </summary>
    [Test]
    public void GenerateTokenExpirationDate()
    {
        Manager.GenerateTokenExpirationDate();

        Assert.Pass();
    }

    /// <summary>
    /// Generates Jwt Claims
    /// </summary>
    [Test]
    public void GenerateJwtClaims()
    {
        var @user = new ApplicationUser
        {
            Id = 7,
            FirstName = "Korbin",
            LastName = "Pacey",
            UserName = "korbin.pacey",
            Email = "korbin.pacey@email.com",
            LastModified = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };

        Manager.GenerateJwtClaims(@user);

        Assert.Pass();
    }

    /// <summary>
    /// Adds Application User Token
    /// </summary>
    [Test]
    public async Task AddApplicationUserToken()
    {
        var @user = new ApplicationUser
        {
            Id = 8,
            FirstName = "Adelyn",
            LastName = "Andre",
            UserName = "Adelyn.Andre",
            Email = "Adelyn.Andre@email.com",
            LastModified = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };

        await Manager.AddApplicationUserToken(@user);

        Assert.Pass();
    }
}