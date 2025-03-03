using System;

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
    }
}
