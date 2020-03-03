using System.Text;

using Hyperdrive.Tier.Settings.Classes;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Hyperdrive.Tier.Web.Extensions
{
    public static class AuthenticationExtension
    {
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
