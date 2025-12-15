using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="ITokenManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface ITokenManager : IBaseManager
    {
        /// <summary>
        /// Generates Token Descriptor
        /// </summary>
        /// <param name="applicationUser">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="SecurityTokenDescriptor"/></returns>
        public SecurityTokenDescriptor GenerateTokenDescriptor(ApplicationUser @applicationUser);

        /// <summary>
        /// Creates Token
        /// </summary>
        /// <param name="securityTokenDescriptor">>Injected <see cref="SecurityTokenDescriptor"/></param>
        /// <returns>Instance of <see cref="string"/></returns>
        public string CreateToken(SecurityTokenDescriptor @securityTokenDescriptor);

        /// <summary>
        /// Generates Symmetric Security Key
        /// </summary>
        /// <returns>Instance of <see cref="SymmetricSecurityKey"/></returns>
        public SymmetricSecurityKey GenerateSymmetricSecurityKey();

        /// <summary>
        /// Generates Signing Credentials
        /// </summary>
        /// <param name="symmetricSecurityKey">>Injected <see cref="SymmetricSecurityKey"/></param>
        /// <returns>Instance of <see cref="SigningCredentials"/></returns>
        public SigningCredentials GenerateSigningCredentials(SymmetricSecurityKey @symmetricSecurityKey);

        /// <summary>
        /// Generates Token Expiration Date 
        /// </summary>
        /// <returns>Instance of <see cref="DateTime"/></returns>
        public DateTime GenerateTokenExpirationDate();

        /// <summary>
        /// Generates Jwt Claims
        /// </summary>
        /// <param name="applicationUser">>Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="List{Claim}"/></returns>
        public List<Claim> GenerateJwtClaims(ApplicationUser @applicationUser);

        /// <summary>
        /// Adds Application User Token
        /// </summary>
        /// <param name="user">>Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="ApplicationUserToken"/></returns>
        public Task<ApplicationUserToken> AddApplicationUserToken(ApplicationUser @user);
    }
}
