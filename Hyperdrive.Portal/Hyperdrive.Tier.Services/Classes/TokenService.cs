using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Hyperdrive.Tier.Services.Classes
{
    public class TokenService : BaseService, ITokenService
    {
        public TokenService(
           IConfiguration configuration) : base(configuration)
        {
        }

        public JwtSecurityToken GenerateJwtToken(ApplicationUser applicationUser)
        {
            return new JwtSecurityToken(
                JwtSettings.JwtIssuer,
                JwtSettings.JwtAudience,
                GenerateJwtClaims(applicationUser),
                expires: GenerateTokenExpirationDate(),
                signingCredentials: GenerateSigningCredentials(GenerateSymmetricSecurityKey())
            );
        }

        public string WriteJwtToken(JwtSecurityToken jwtSecurityToken) => new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        public SymmetricSecurityKey GenerateSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtKey));
        }

        public SigningCredentials GenerateSigningCredentials(SymmetricSecurityKey symmetricSecurityKey)
        {
            return new SigningCredentials(symmetricSecurityKey,
                                          SecurityAlgorithms.HmacSha256);
        }

        public DateTime GenerateTokenExpirationDate() => DateTime.Now.AddDays(JwtSettings.JwtExpireDays);

        public List<Claim> GenerateJwtClaims(ApplicationUser applicationUser)
        {
            return new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    applicationUser.Email),
                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
                new Claim(
                    ClaimTypes.NameIdentifier,
                    applicationUser.Id.ToString()),
                new Claim(
                    ClaimTypes.Email,
                    applicationUser.Email),
                new Claim(
                    JwtRegisteredClaimNames.Iss,
                    JwtSettings.JwtIssuer),
                new Claim(
                    JwtRegisteredClaimNames.Aud,
                    JwtSettings.JwtAudience),
            };
        }
    }
}
