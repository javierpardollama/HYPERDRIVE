using System.Text;
using Hyperdrive.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Hyperdrive.Application.Installers;

/// <summary>
///     Represents a <see cref="AuthenticationInstaller" /> class.
/// </summary>
public static class AuthenticationInstaller
{
    /// <summary>
    ///     Installs Authentication
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="JwtSettings" /></param>
    public static void InstallAuthentication(this IServiceCollection @this, JwtSettings @settings)
    {
        @this.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.ClaimsIssuer = @settings.JwtIssuer;
            options.Authority = @settings.JwtAuthority;
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
                ValidIssuer = @settings.JwtIssuer,
                ValidAudiences = @settings.JwtAudiences,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@settings.JwtKey))
            };
        });
    }
}