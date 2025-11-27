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

        Context = new ApplicationContext(ContextOptionsBuilder.Options);
        Context.Seed();

        InstallLogger();

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
            CreatedAt = DateTime.UtcNow,
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
            CreatedAt = DateTime.UtcNow,
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
    [Test]
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
            CreatedAt = DateTime.UtcNow,
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
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            SecurityStamp = new Guid().ToString()
        };

        await Manager.AddApplicationUserToken(@user);

        Assert.Pass();
    }
}