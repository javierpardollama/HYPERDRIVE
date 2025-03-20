using Hyperdrive.Tier.Middlewares.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace Hyperdrive.Tier.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="MiddlewareExtension"/> class.
    /// </summary>
    public static class MiddlewareExtension
    {
        /// <summary>
        /// Extends Customized Exception MiddleWare
        /// </summary>
        /// <param name="this">Injected <see cref="WebApplication"/></param>
        public static void UseCustomizedMiddlewares(this WebApplication @this)
        {
            @this.UseMiddleware<HeaderMiddleware>();
        }
    }
}
