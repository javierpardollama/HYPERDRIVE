using Hyperdrive.Main.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Hyperdrive.Main.Application.Installers;

/// <summary>
///     Represents a <see cref="IdentificationInstaller" /> class.
/// </summary>
public static class IdentificationInstaller
{
    /// <summary>
    ///     Installs Identification
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="JwtSettings" /></param>
    public static void InstallIdentification(this IServiceCollection @this, JwtSettings @settings)
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
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = @settings.JwtIssuer,
                ValidAudiences = @settings.JwtAudiences,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@settings.JwtKey))
            };
        });
    }

    /// <summary>
    ///     Uses Identification
    /// </summary>
    /// <param name="this">Injected <see cref="WebApplication" /></param>
    public static void UseIdentification(this WebApplication @this)
    {
        @this.UseAuthentication();
        @this.UseAuthorization();
    }
}