using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Settings;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Hyperdrive.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="RefreshTokenManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IRefreshTokenManager"/>
/// </summary>   
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger{RefreshTokenManager}"/></param>
/// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>

public class RefreshTokenManager(IApplicationContext @context,
                                 ILogger<RefreshTokenManager> @logger,
                                 IOptions<JwtSettings> @jwtSettings) : BaseManager(@context, @jwtSettings), IRefreshTokenManager
{
    /// <summary>
    /// Generates Jwt Refresh Token Expiration Date 
    /// </summary>
    /// <returns>Instance of <see cref="DateTime"/></returns>
    public DateTime GenerateRefreshTokenExpirationDate() => DateTime.UtcNow.AddDays(JwtSettings.Value.JwtExpireDays);

    /// <summary>
    /// Writes Jwt Refresh Token
    /// </summary>
    /// <returns>Instance of <see cref="string"/></returns>
    public string WriteJwtRefreshToken() => StringHelper.HashString(StringHelper.GetRandomizedString());

    /// <summary>
    /// Checks whether Jwt Refresh Token is Revoked
    /// </summary>
    /// <param name="userid">Injected <see cref="int"/></param>
    /// <param name="token">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task IsRevoked(int @userid, string @token)
    {
        ApplicationUserRefreshToken @refreshToken = await FindApplicationUserRefreshTokenByCredentials(@userid, @token);

        if (@refreshToken is null) throw new UnauthorizedAccessException("Invalid Token");

        if (@refreshToken.Revoked) throw new UnauthorizedAccessException("Revoked Token");

        if (@refreshToken.ExpiresAt < DateTime.UtcNow) throw new UnauthorizedAccessException("Expired Token");
    }

    /// <summary>
    /// Revokes Jwt Refresh Token
    /// </summary>      
    /// <param name="userid">Injected <see cref="int"/></param>
    /// <param name="token">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Revoke(int @userid, string @token)
    {
        ApplicationUserRefreshToken @refreshToken = await FindApplicationUserRefreshTokenByCredentials(@userid, @token);

        @refreshToken.Revoked = true;
        @refreshToken.RevokedAt = DateTime.UtcNow;

        Context.UserRefreshTokens.Update(@refreshToken);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(ApplicationUserRefreshToken)               
            + " was revoked at "
            + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);
    }

    /// <summary>
    /// Finds Application User Refresh Token By User Id
    /// </summary>
    /// <param name="userid">Injected <see cref="string"/></param>
    /// <param name="token">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="ApplicationUserRefreshToken"/></returns>
    public async Task<ApplicationUserRefreshToken> FindApplicationUserRefreshTokenByCredentials(int @userid, string @token)
    {
        ApplicationUserRefreshToken @refreshToken = await Context.UserRefreshTokens
            .TagWith("FindApplicationUserRefreshTokenByCredentials")
            .Include(x => x.User)
            .Where(x => x.UserId == @userid && x.Value == @token)
            .FirstOrDefaultAsync();

        if (@refreshToken is null)
        {
            // Log
            string @logData = nameof(ApplicationUserRefreshToken)                 
                + " was not found at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.LogError(@logData);
        }

        return @refreshToken;
    }

    /// <summary>
    /// Adds Application User Refresh Token
    /// </summary>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <returns>Instance of <see cref="ApplicationUserToken"/></returns>
    public async Task<ApplicationUserRefreshToken> AddApplicationUserRefreshToken(ApplicationUser @user)
    {
        ApplicationUserRefreshToken @refreshToken = new()
        {
            Name = Guid.NewGuid().ToString(),
            LoginProvider = JwtSettings.Value.JwtIssuer,
            UserId = @user.Id,             
            Value = WriteJwtRefreshToken(),
            ExpiresAt = GenerateRefreshTokenExpirationDate()               
        };
        
        await Context.UserRefreshTokens.AddAsync(@refreshToken);
        
        await Context.SaveChangesAsync();
        
        // Log
        string @logData = nameof(ApplicationUserRefreshToken)                             
                          + " was added at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @refreshToken;
    }
}
