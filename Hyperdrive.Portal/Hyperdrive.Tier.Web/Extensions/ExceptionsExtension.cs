using Hyperdrive.Tier.ExceptionHandling.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace Hyperdrive.Tier.Web.Extensions
{
    public static class ExceptionsExtension
    {
        public static void UseCustomizedExceptionMiddlewares(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
