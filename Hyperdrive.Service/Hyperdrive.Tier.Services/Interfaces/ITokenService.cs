using Hyperdrive.Tier.Entities.Classes;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hyperdrive.Tier.Services.Interfaces
{
    /// <summary>
    /// Represents a <see cref="ITokenService"/> interface. Inherits <see cref="IBaseService"/>
    /// </summary>
    public interface ITokenService : IBaseService
    {
        /// <summary>
        /// Generates Token Descriptor
        /// </summary>
        /// <param name="applicationUser">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="SecurityTokenDescriptor"/></returns>
        SecurityTokenDescriptor GenerateTokenDescriptor(ApplicationUser @applicationUser);

        /// <summary>
        /// Creates Token
        /// </summary>
        /// <param name="securityTokenDescriptor">>Injected <see cref="SecurityTokenDescriptor"/></param>
        /// <returns>Instance of <see cref="string"/></returns>
        string CreateToken(SecurityTokenDescriptor @securityTokenDescriptor);

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
