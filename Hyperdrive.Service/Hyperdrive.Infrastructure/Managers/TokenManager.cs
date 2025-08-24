using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Settings;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Hyperdrive.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Hyperdrive.Infrastructure.Managers
{
    /// <summary>
    /// Represents a <see cref="TokenManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="ITokenManager"/>
    /// </summary>   
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="logger">Injected <see cref="ILogger{RefreshTokenService}"/></param>
    /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
    public class TokenManager(IApplicationContext @context,
        ILogger<TokenManager> @logger,
        IOptions<JwtSettings> @jwtSettings) : BaseManager(@context, @jwtSettings), ITokenManager
    {

        /// <summary>
        /// Generates Token Descriptor
        /// </summary>
        /// <param name="applicationUser">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="SecurityTokenDescriptor"/></returns>
        public SecurityTokenDescriptor GenerateTokenDescriptor(ApplicationUser @applicationUser) => new()
        {
            Issuer = @jwtSettings.Value.JwtIssuer,
            Claims = ClaimHelper.ToDictionary(GenerateJwtClaims(applicationUser)),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(@jwtSettings.Value.JwtExpireMinutes),
            SigningCredentials = GenerateSigningCredentials(GenerateSymmetricSecurityKey())
        };

        /// <summary>
        /// Creates Token
        /// </summary>
        /// <param name="securityTokenDescriptor">>Injected <see cref="SecurityTokenDescriptor"/></param>
        /// <returns>Instance of <see cref="string"/></returns>
        public string CreateToken(SecurityTokenDescriptor @securityTokenDescriptor) => new JsonWebTokenHandler().CreateToken(@securityTokenDescriptor);

        /// <summary>
        /// Generates Symmetric Security Key
        /// </summary>
        /// <returns>Instance of <see cref="SymmetricSecurityKey"/></returns>
        public SymmetricSecurityKey GenerateSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(JwtSettings.Value.JwtKey));

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
        public DateTime GenerateTokenExpirationDate() => DateTime.UtcNow.AddMinutes(JwtSettings.Value.JwtExpireMinutes);

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
                    $"{@applicationUser.Email}"),
                new(
                    JwtRegisteredClaimNames.Email,
                    $"{@applicationUser.Email}"),
                new(
                    JwtRegisteredClaimNames.UniqueName,
                    $"{@applicationUser.Email}"),
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
                    JwtRegisteredClaimNames.Alg,
                    SecurityAlgorithms.HmacSha256Signature),
            }.Union(JwtSettings.Value.JwtAudiences
                .Select(@audience => new Claim(JwtRegisteredClaimNames.Aud, @audience)))
             .Union(@applicationUser.ApplicationUserRoles
                .Select(@applicationUserRole => new Claim(ClaimTypes.Role, $"{@applicationUserRole?.ApplicationRole?.Name }")))];
        
        /// <summary>
        /// Adds Application User Token
        /// </summary>
        /// <param name="user">>Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="ApplicationUserToken"/></returns>
        public async Task<ApplicationUserToken> AddApplicationUserToken(ApplicationUser @user)
        {
            ApplicationUserToken @userToken = new ApplicationUserToken
            {
                Name = Guid.NewGuid().ToString(),
                LoginProvider = JwtSettings.Value.JwtIssuer,
                ApplicationUser = @user,
                UserId = @user.Id,
                Value = CreateToken(GenerateTokenDescriptor(@user))
            };
            
            await Context.UserTokens.AddAsync(@userToken);
            
            await Context.SaveChangesAsync();
            
            // Log
            string @logData = nameof(ApplicationUserToken)
                              + " with Id "
                              + @userToken.Id
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @userToken;
        }
    }
}

