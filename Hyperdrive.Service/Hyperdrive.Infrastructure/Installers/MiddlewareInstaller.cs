using Hyperdrive.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Hyperdrive.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="MiddlewareInstaller" /> class.
/// </summary>
public static class MiddlewareInstaller
{
    /// <summary>
    ///     Use Middlewares
    /// </summary>
    /// <param name="this">Injected <see cref="WebApplication" /></param>
    public static void UseMiddlewares(this WebApplication @this)
    {
        @this.UseMiddleware<HeaderMiddleware>();

        // Add other services here
    }
}