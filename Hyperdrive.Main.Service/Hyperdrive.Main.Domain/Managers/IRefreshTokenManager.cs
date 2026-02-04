using Hyperdrive.Main.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Domain.Managers
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
        public string WriteJwtRefreshToken();

        /// <summary>
        /// Generates Jwt Refresh Token Expiration Date 
        /// </summary>
        /// <returns>Instance of <see cref="DateTime"/></returns>
        public DateTime GenerateRefreshTokenExpirationDate();

        /// <summary>
        /// Checks whether Jwt Refresh Token is Revoked
        /// </summary>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public Task IsRevoked(int @userid, string @token);

        /// <summary>
        /// Revokes Jwt Refresh Token
        /// </summary>      
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public Task Revoke(int @userid, string @token);

        /// <summary>
        /// Finds Application User Refresh Token By User Id
        /// </summary>
        /// <param name="userid">Injected <see cref="string"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="ApplicationUserRefreshToken"/></returns>
        public Task<ApplicationUserRefreshToken> FindApplicationUserRefreshTokenByCredentials(int @userid, string @token);

        /// <summary>
        /// Adds Application User Refresh Token
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="ApplicationUserRefreshToken"/></returns>
        public Task<ApplicationUserRefreshToken> AddApplicationUserRefreshToken(ApplicationUser @user);
    }
}
