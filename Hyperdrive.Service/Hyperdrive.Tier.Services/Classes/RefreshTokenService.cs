﻿using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Helpers.Classes;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Hyperdrive.Tier.Exceptions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="RefreshTokenService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IRefreshTokenService"/>
    /// </summary>   
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="logger">Injected <see cref="ILogger{RefreshTokenService}"/></param>
    /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
    /// <param name="userManager">Injected <see cref="UserManager{ApplicationUser}"/></param>
    public class RefreshTokenService(IApplicationContext @context,
                                     ILogger<RefreshTokenService> @logger,
                                     IOptions<JwtSettings> @jwtSettings,
                                     UserManager<ApplicationUser> @userManager) : BaseService(@context, @jwtSettings), IRefreshTokenService
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
        /// <param name="viewModel">Injected <see cref="SecurityRefreshTokenReset"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task IsRevoked(SecurityRefreshTokenReset @viewModel)
        {
            ApplicationUserRefreshToken @refreshToken = await FindApplicationUserRefreshTokenByApplicationUserId(@viewModel.ApplicationUserId, @viewModel.ApplicationUserRefreshToken);

            if (@refreshToken is null) throw new UnauthorizedAccessException("Invalid Token");

            if (@refreshToken.Revoked) throw new UnauthorizedAccessException("Revoked Token");

            if (@refreshToken.ExpiresAt < DateTime.UtcNow) throw new UnauthorizedAccessException("Expired Token");
        }

        /// <summary>
        /// Revokes Jwt Refresh Token
        /// </summary>      
        /// <param name="viewModel">Injected <see cref="SecurityRefreshTokenReset"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task Revoke(SecurityRefreshTokenReset @viewModel)
        {
            ApplicationUserRefreshToken @refreshToken = await FindApplicationUserRefreshTokenByApplicationUserId(@viewModel.ApplicationUserId, @viewModel.ApplicationUserRefreshToken);

            @refreshToken.Revoked = true;
            @refreshToken.RevokedAt = DateTime.UtcNow;

            Context.UserRefreshTokens.Update(@refreshToken);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(ApplicationUserRefreshToken)
                + " with Id "
                + @refreshToken.Id
                + " was revoked at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.WriteRefreshTokenRevokedLog(@logData);
        }

        /// <summary>
        /// Finds Application User Refresh Token By User Id
        /// </summary>
        /// <param name="userid">Injected <see cref="string"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="ApplicationUserRefreshToken"/></returns>
        public async Task<ApplicationUserRefreshToken> FindApplicationUserRefreshTokenByApplicationUserId(int @userid, string @token)
        {
            ApplicationUserRefreshToken @refreshToken = await Context.UserRefreshTokens
                .TagWith("FindApplicationUserRefreshTokenByApplicationUserId")
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.ApplicationUser.Id == @userid && x.Value == @token);

            if (@refreshToken is null)
            {
                // Log
                string @logData = nameof(ApplicationUserRefreshToken)
                    + " with User Id "
                    + @userid
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);
            }

            return @refreshToken;
        }

        /// <summary>
        /// Adds Application User Refresh Token
        /// </summary>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="ApplicationUserToken"/></returns>
        public async Task<ApplicationUserRefreshToken> AddApplicationUserRefreshToken(int userid)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(userid);
            
            ApplicationUserRefreshToken @refreshToken = new ApplicationUserRefreshToken
            {
                Name = Guid.NewGuid().ToString(),
                LoginProvider = JwtSettings.Value.JwtIssuer,
                ApplicationUser = @applicationUser,
                Value = WriteJwtRefreshToken(),
                ExpiresAt = GenerateRefreshTokenExpirationDate()
            };
            
            await Context.UserRefreshTokens.AddAsync(@refreshToken);
            
            await Context.SaveChangesAsync();
            
            // Log
            string @logData = nameof(ApplicationUserRefreshToken)
                              + " with Id "
                              + @refreshToken.Id
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.WriteInsertItemLog(@logData);

            return @refreshToken;
        }

      
        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> FindApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await @userManager.Users
                .TagWith("FindApplicationUserById")
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(@applicationUser)
                                  + " with Id "
                                  + @id
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                                           + " with Id "
                                           + @id
                                           + " does not exist");
            }

            return @applicationUser;
        }
    }
}
