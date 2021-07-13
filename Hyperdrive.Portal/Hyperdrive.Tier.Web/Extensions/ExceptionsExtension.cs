using Hyperdrive.Tier.ExceptionHandling.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace Hyperdrive.Tier.Web.Extensions
{
    /// <summary>
    /// Represents a <see cref="ExceptionsExtension"/> class.
    /// </summary>
    public static class ExceptionsExtension
    {
        /// <summary>
        /// Extends Customized Exception MiddleWare
        /// </summary>
        /// <param name="this">Injected <see cref="IApplicationBuilder"/></param>
        public static void UseCustomizedExceptionMiddlewares(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
