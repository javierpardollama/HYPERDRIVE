using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Helpers.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Exceptions.Exceptions;
using Hyperdrive.Tier.Logging.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="TokenService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="ITokenService"/>
    /// </summary>   
    /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
    public class TokenService(IApplicationContext @context,
        ILogger<TokenService> @logger,
        IOptions<JwtSettings> @jwtSettings,
        UserManager<ApplicationUser> @userManager) : BaseService(@context, @jwtSettings), ITokenService
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
        
        public async Task<ApplicationUserToken> AddApplicationUserToken(int userid)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(userid);

            ApplicationUserToken @userToken = new ApplicationUserToken
            {
                Name = Guid.NewGuid().ToString(),
                LoginProvider = JwtSettings.Value.JwtIssuer,
                ApplicationUser = @applicationUser,
                UserId = @applicationUser.Id,
                Value = CreateToken(GenerateTokenDescriptor(@applicationUser))
            };
            
            await Context.UserTokens.AddAsync(@userToken);
            
            await Context.SaveChangesAsync();
            
            // Log
            string @logData = nameof(ApplicationUserToken)
                              + " with Id "
                              + @userToken.Id
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.WriteInsertItemLog(@logData);

            return @userToken;
        }
        
        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task<ApplicationUser> FindApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await @userManager.Users
                .TagWith("FindApplicationUserById")
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(@applicationUser)
                                  + " with Id "
                                  + @id
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                                           + " with Id "
                                           + @id
                                           + " does not exist");
            }

            return @applicationUser;
        }

    }
}

