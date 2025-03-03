using Hyperdrive.Tier.Entities.Classes;

using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hyperdrive.Tier.Services.Interfaces
{
    /// <summary>
    /// Represents a <see cref="ITokenService"/> interface. Inherits <see cref="IBaseService"/>
    /// </summary>
    public interface ITokenService : IBaseService
    {
        /// <summary>
        /// Generates Jwt Token
        /// </summary>
        /// <param name="applicationUser">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="JwtSecurityToken"/></returns>
        JwtSecurityToken GenerateJwtToken(ApplicationUser @applicationUser);

        /// <summary>
        /// Writes Jwt Token
        /// </summary>
        /// <param name="jwtSecurityToken">>Injected <see cref="JwtSecurityToken"/></param>
        /// <returns>Instance of <see cref="string"/></returns>
        string WriteJwtToken(JwtSecurityToken @jwtSecurityToken);      

        /// <summary>
        /// Generates Symmetric Security Key
        /// </summary>
        /// <returns>Instance of <see cref="SymmetricSecurityKey"/></returns>
        SymmetricSecurityKey GenerateSymmetricSecurityKey();

        /// <summary>
        /// Generates Signing Credentials
        /// </summary>
        /// <param name="symmetricSecurityKey">>Injected <see cref="SymmetricSecurityKey"/></param>
        /// <returns>Instance of <see cref="SigningCredentials"/></returns>
        SigningCredentials GenerateSigningCredentials(SymmetricSecurityKey @symmetricSecurityKey);

        /// <summary>
        /// Generates Token Expiration Date 
        /// </summary>
        /// <returns>Instance of <see cref="DateTime"/></returns>
        DateTime GenerateTokenExpirationDate();

        /// <summary>
        /// Generates Jwt Claims
        /// </summary>
        /// <param name="applicationUser">>Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="List{Claim}"/></returns>
        List<Claim> GenerateJwtClaims(ApplicationUser @applicationUser);
    }
}
