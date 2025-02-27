﻿using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="TokenService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="ITokenService"/>
    /// </summary>   
    /// <param name="logger">Injected <see cref="ILogger{TokenService}"/></param>
    /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
    public class TokenService(ILogger<TokenService> @logger,
                              IOptions<JwtSettings> @jwtSettings) : BaseService(@logger, @jwtSettings), ITokenService
    {

        /// <summary>
        /// Generates Jwt Token
        /// </summary>
        /// <param name="applicationUser">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="JwtSecurityToken"/></returns>
        public JwtSecurityToken GenerateJwtToken(ApplicationUser @applicationUser) => new(issuer: JwtSettings.Value.JwtIssuer,
                                                                                          audience: null,
                                                                                          claims: GenerateJwtClaims(@applicationUser),
                                                                                          expires: GenerateTokenExpirationDate(),
                                                                                          signingCredentials: GenerateSigningCredentials(GenerateSymmetricSecurityKey()));

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
        public SymmetricSecurityKey GenerateSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Value.JwtKey));

        /// <summary>
        /// Generates Signing Credentials
        /// </summary>
        /// <param name="symmetricSecurityKey">>Injected <see cref="SymmetricSecurityKey"/></param>
        /// <returns>Instance of <see cref="SigningCredentials"/></returns>
        public SigningCredentials GenerateSigningCredentials(SymmetricSecurityKey @symmetricSecurityKey) => new(@symmetricSecurityKey,
                                                                                                                SecurityAlgorithms.HmacSha256Signature);

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
        public List<Claim> GenerateJwtClaims(ApplicationUser @applicationUser) => [.. new List<Claim>
            {
                new(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
                new(
                    JwtRegisteredClaimNames.Sub,
                    @applicationUser.Id.ToString()),
                new(
                    ClaimTypes.Email,
                    @applicationUser.Email),
                new(
                    JwtRegisteredClaimNames.Email,
                    @applicationUser.Email),
                new(
                    JwtRegisteredClaimNames.UniqueName,
                    @applicationUser.Email),
                new(
                    JwtRegisteredClaimNames.EmailVerified,
                    @applicationUser.EmailConfirmed.ToString()),
                new(
                    ClaimTypes.Name,
                    $"{@applicationUser.FirstName} {applicationUser.LastName}"),
                new(
                    JwtRegisteredClaimNames.Name,
                    $"{@applicationUser.FirstName} {applicationUser.LastName}"),
                new(
                    ClaimTypes.GivenName,
                    $"{@applicationUser.FirstName}"),
                new(
                    JwtRegisteredClaimNames.GivenName,
                    $"{@applicationUser.FirstName}"),
                new(
                    ClaimTypes.Surname,
                    $"{applicationUser.LastName}"),
                new(
                    JwtRegisteredClaimNames.FamilyName,
                    $"{applicationUser.LastName}"),
                new(
                    ClaimTypes.MobilePhone,
                    $"{applicationUser.PhoneNumber}"),
                new(
                    JwtRegisteredClaimNames.PhoneNumber,
                    $"{applicationUser.PhoneNumber}"),
                new(
                    JwtRegisteredClaimNames.PhoneNumberVerified,
                    @applicationUser.PhoneNumberConfirmed.ToString()),
                new(
                    JwtRegisteredClaimNames.UpdatedAt,
                    new DateTimeOffset(@applicationUser.LastModified).ToUnixTimeSeconds().ToString()),
                new(
                    JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                new(
                    JwtRegisteredClaimNames.Alg,
                    SecurityAlgorithms.HmacSha256Signature),
            }.Union(JwtSettings.Value.JwtAudiences
                .Select(@audience => new Claim(JwtRegisteredClaimNames.Aud, @audience)))
             .Union(@applicationUser.ApplicationUserRoles
                .Select(@applicationUserRole => new Claim(ClaimTypes.Role, @applicationUserRole?.ApplicationRole?.Name)))];
    }
}

