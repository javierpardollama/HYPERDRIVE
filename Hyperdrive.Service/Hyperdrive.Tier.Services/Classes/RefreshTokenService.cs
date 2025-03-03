using Hyperdrive.Tier.Helpers.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="RefreshTokenService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IRefreshTokenService"/>
    /// </summary>   
    /// <param name="logger">Injected <see cref="ILogger{RefreshTokenService}"/></param>
    /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
    public class RefreshTokenService(ILogger<RefreshTokenService> @logger,
                              IOptions<JwtSettings> @jwtSettings) : BaseService(@logger, @jwtSettings), IRefreshTokenService
    {
        /// <summary>
        /// Generates Jwt Refresh Token Expiration Date 
        /// </summary>
        /// <returns>Instance of <see cref="DateTime"/></returns>
        public DateTime GenerateRefreshTokenExpirationDate() => DateTime.Now.AddDays(JwtSettings.Value.JwtExpireDays);     

        /// <summary>
        /// Writes Jwt Refresh Token
        /// </summary>
        /// <returns>Instance of <see cref="string"/></returns>
        public string WriteJwtRefreshToken() => StringHelper.HashString(StringHelper.GetRandomizedString());

    }
}
