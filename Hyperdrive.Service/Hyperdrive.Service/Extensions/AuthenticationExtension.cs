using Hyperdrive.Tier.Settings.Classes;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace Hyperdrive.Tier.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="ContextsExtension"/> class.
    /// </summary>
    public static class AuthenticationExtension
    {
        /// <summary>
        /// Extends Customized Authentication
        /// </summary>
        /// <param name="this">Injected <see cref="IServiceCollection"/></param>
        /// <param name="JwtSettings">Injected <see cref="JwtSettings"/></param>
        public static void AddCustomizedAuthentication(this IServiceCollection @this, JwtSettings @JwtSettings)
        {
            @this.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.ClaimsIssuer = @JwtSettings.JwtIssuer;
                   options.Authority = @JwtSettings.JwtAuthority;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       RequireAudience = true,
                       RequireExpirationTime = true,
                       RequireSignedTokens = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = @JwtSettings.JwtIssuer,
                       ValidAudiences = @JwtSettings.JwtAudiences,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@JwtSettings.JwtKey))                       
                   };
               });
        }
    }
}
