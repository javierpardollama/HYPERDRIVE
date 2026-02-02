using Hyperdrive.Ai.Application.Handlers;
using Hyperdrive.Ai.Domain.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Hyperdrive.Ai.Application.Installers;

/// <summary>
///     Represents a <see cref="IdentificationInstaller" /> class.
/// </summary>
public static class IdentificationInstaller
{
    /// <summary>
    ///     Installs Authentication
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="ApiSettings" /></param>
    public static void InstallIdentification(this IServiceCollection @this, ApiSettings @settings)
    {
        @this.AddAuthentication(nameof(AuthenticationSchemes.Basic))
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(nameof(AuthenticationSchemes.Basic),
                options => options.ClaimsIssuer = @settings.ApiIssuer);
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