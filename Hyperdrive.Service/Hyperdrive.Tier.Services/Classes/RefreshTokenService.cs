using Hyperdrive.Tier.Helpers.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Hyperdrive.Tier.Services.Classes
{
    public class RefreshTokenService(ILogger<RefreshTokenService> @logger,
                              IOptions<JwtSettings> @jwtSettings) : BaseService(@logger, @jwtSettings), IRefreshTokenService
    {
        public DateTime GenerateRefreshTokenExpirationDate() => DateTime.Now.AddDays(JwtSettings.Value.JwtExpireDays);     

        /// <summary>
        /// Writes Jwt Refresh Token
        /// </summary>
        /// <returns>Instance of <see cref="string"/></returns>
        public string WriteJwtRefreshToken() => StringHelper.HashString(StringHelper.GetRandomizedString());

    }
}
