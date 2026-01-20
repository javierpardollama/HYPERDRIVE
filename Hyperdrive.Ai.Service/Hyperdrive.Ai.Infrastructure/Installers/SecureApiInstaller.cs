using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Hyperdrive.Ai.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="SecureApiInstaller" /> class.
/// </summary>
public static class SecureApiInstaller
{
    /// <summary>
    ///     Installs Secure Api
    /// </summary>
    /// <param name="this">Injected <see cref="WebApplicationBuilder" /></param>
    public static void InstallSecureApi(this WebApplicationBuilder @this)
    {
        @this.Services.AddHsts(options =>
        {
            options.Preload = true; // For browser HSTS preload lists
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(365); // Recommended: at least 1 year
        });

        @this.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.AddServerHeader = false; // Turn off Server header
        });
    }

    /// <summary>
    ///     Uses Secure Api
    /// </summary>
    /// <param name="this">Injected <see cref="WebApplication" /></param>
    public static void UseSecureApi(this WebApplication @this)
    {
        if (!@this.Environment.IsDevelopment())
        {
            @this.UseHsts();
        }

        @this.UseHttpsRedirection();
    }
}