using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Interfaces
{
    /// <summary>
    /// Represents a <see cref="IRefreshTokenService"/> interface. Inherits <see cref="IBaseService"/>
    /// </summary>
    public interface IRefreshTokenService : IBaseService
    {
        /// <summary>
        /// Writes Jwt Refresh Token
        /// </summary>
        /// <returns>Instance of <see cref="string"/></returns>
        string WriteJwtRefreshToken();

        /// <summary>
        /// Generates Jwt Refresh Token Expiration Date 
        /// </summary>
        /// <returns>Instance of <see cref="DateTime"/></returns>
        DateTime GenerateRefreshTokenExpirationDate();

        /// <summary>
        /// Checks whether Jwt Refresh Token is Revoked
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityRefreshTokenReset"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task IsRevoked(SecurityRefreshTokenReset @viewModel);

        /// <summary>
        /// Revokes Jwt Refresh Token
        /// </summary>      
        /// <param name="viewModel">Injected <see cref="SecurityRefreshTokenReset"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task Revoke(SecurityRefreshTokenReset @viewModel);

        /// <summary>
        /// Finds Application User Refresh Token By User Id
        /// </summary>
        /// <param name="userid">Injected <see cref="string"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="ApplicationUserRefreshToken"/></returns>
        Task<ApplicationUserRefreshToken> FindApplicationUserRefreshTokenByApplicationUserId(int @userid, string @token);
        
        /// <summary>
        /// Adds Application User Refresh Token
        /// </summary>
        /// <param name="userid">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="ApplicationUserRefreshToken"/></returns>
        Task<ApplicationUserRefreshToken> AddApplicationUserRefreshToken(int @userid);

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        Task<ApplicationUser> FindApplicationUserById(int @userid);
    }
}
