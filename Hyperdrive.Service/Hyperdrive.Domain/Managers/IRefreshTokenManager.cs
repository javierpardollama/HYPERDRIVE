using System;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IRefreshTokenManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IRefreshTokenManager : IBaseManager
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
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task IsRevoked(int @id, string @token);

        /// <summary>
        /// Revokes Jwt Refresh Token
        /// </summary>      
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task Revoke(int @id, string @token);

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
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="ApplicationUserRefreshToken"/></returns>
        Task<ApplicationUserRefreshToken> AddApplicationUserRefreshToken(ApplicationUser @user);
    }
}
