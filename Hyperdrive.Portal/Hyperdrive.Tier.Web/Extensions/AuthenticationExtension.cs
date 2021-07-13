using System.Text;

using Hyperdrive.Tier.Settings.Classes;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Hyperdrive.Tier.Web.Extensions
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
        public static void AddCustomizedAuthentication(this IServiceCollection @this, JwtSettings JwtSettings)
        {
            @this.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       ValidIssuer = JwtSettings.JwtIssuer,
                       ValidAudience = JwtSettings.JwtAudience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtKey))
                   };
               });
        }
    }
}
