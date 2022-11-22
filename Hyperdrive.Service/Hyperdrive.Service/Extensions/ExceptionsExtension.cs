using Hyperdrive.Tier.ExceptionHandling.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace Hyperdrive.Tier.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="ExceptionsExtension"/> class.
    /// </summary>
    public static class ExceptionsExtension
    {
        /// <summary>
        /// Extends Customized Exception MiddleWare
        /// </summary>
        /// <param name="this">Injected <see cref="WebApplication"/></param>
        public static void UseCustomizedExceptionMiddlewares(this WebApplication @this)
        {
            @this.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
