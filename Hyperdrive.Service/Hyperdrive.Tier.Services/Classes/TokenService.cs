﻿using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="TokenService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="ITokenService"/>
    /// </summary>
    public class TokenService : BaseService, ITokenService
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="TokenService"/>
        /// </summary>
        /// <param name="logger">Injected <see cref="ILogger{TokenService}"/></param>
        /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
        public TokenService(
            ILogger<TokenService> @logger,
            IOptions<JwtSettings> @jwtSettings) : base(@logger, @jwtSettings)
        {
        }

        /// <summary>
        /// Generates Jwt Token
        /// </summary>
        /// <param name="applicationUser">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="JwtSecurityToken"/></returns>
        public JwtSecurityToken GenerateJwtToken(ApplicationUser @applicationUser)
        {
            return new JwtSecurityToken(
                issuer: JwtSettings.Value.JwtIssuer,
                audience: null,
                claims: GenerateJwtClaims(@applicationUser),
                expires: GenerateTokenExpirationDate(),
                signingCredentials: GenerateSigningCredentials(GenerateSymmetricSecurityKey())
            );
        }

        /// <summary>
        /// Writes Jwt Token
        /// </summary>
        /// <param name="jwtSecurityToken">>Injected <see cref="JwtSecurityToken"/></param>
        /// <returns>Instance of <see cref="string"/></returns>
        public string WriteJwtToken(JwtSecurityToken @jwtSecurityToken) => new JwtSecurityTokenHandler().WriteToken(@jwtSecurityToken);

        /// <summary>
        /// Generates Symmetric Security Key
        /// </summary>
        /// <returns>Instance of <see cref="SymmetricSecurityKey"/></returns>
        public SymmetricSecurityKey GenerateSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Value.JwtKey));
        }

        /// <summary>
        /// Generates Signing Credentials
        /// </summary>
        /// <param name="symmetricSecurityKey">>Injected <see cref="SymmetricSecurityKey"/></param>
        /// <returns>Instance of <see cref="SigningCredentials"/></returns>
        public SigningCredentials GenerateSigningCredentials(SymmetricSecurityKey @symmetricSecurityKey)
        {
            return new SigningCredentials(@symmetricSecurityKey,
                                          SecurityAlgorithms.EcdsaSha512Signature);
        }

        /// <summary>
        /// Generates Token Expiration Date 
        /// </summary>
        /// <returns>Instance of <see cref="DateTime"/></returns>
        public DateTime GenerateTokenExpirationDate() => DateTime.Now.AddMinutes(JwtSettings.Value.JwtExpireMinutes);

        /// <summary>
        /// Generates Jwt Claims
        /// </summary>
        /// <param name="applicationUser">>Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="List{Claim}"/></returns>
        public List<Claim> GenerateJwtClaims(ApplicationUser @applicationUser)
        {
            return new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
                new Claim(
                    ClaimTypes.Sid,
                    @applicationUser.Id.ToString()),
                new Claim(
                    ClaimTypes.Email,
                    @applicationUser.Email),
                new Claim(
                    ClaimTypes.Name,
                    @applicationUser.FirstName),
                new Claim(
                    ClaimTypes.Surname,
                    @applicationUser.LastName),
                new Claim(
                    ClaimTypes.MobilePhone,
                    @applicationUser.PhoneNumber),
                new Claim(
                    JwtRegisteredClaimNames.Iss,
                    JwtSettings.Value.JwtIssuer),
                new Claim(
                    JwtRegisteredClaimNames.AuthTime,
                    DateTime.Now.ToString()),
                new Claim(
                    JwtRegisteredClaimNames.Iat,
                    DateTime.Now.ToString()),
                new Claim(
                    JwtRegisteredClaimNames.Exp,
                    GenerateTokenExpirationDate().ToString()),
                new Claim(
                    ClaimTypes.System,
                    Environment.MachineName),
                new Claim(
                    ClaimTypes.Version,
                    Environment.OSVersion.VersionString),
                new Claim(
                    ClaimTypes.Locality,
                    CultureInfo.CurrentCulture.TwoLetterISOLanguageName),
            }.Union(JwtSettings.Value.JwtAudiences
                .Select(@audience => new Claim(JwtRegisteredClaimNames.Aud, @audience)))
             .Union(@applicationUser.ApplicationUserRoles
                .Select(@applicationUserRole => new Claim(ClaimTypes.Role, @applicationUserRole?.ApplicationRole?.Name)))
             .ToList();
        }
    }
}

