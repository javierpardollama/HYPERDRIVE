using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Hyperdrive.Tier.Middlewares.Middlewares
{
    /// <summary>
    /// Represents a <see cref="HeaderMiddleware"/> class
    /// </summary>   
    /// <param name="request">Injected <see cref="RequestDelegate"/></param>
    public class HeaderMiddleware(RequestDelegate request)
    {
        /// <summary>
        /// Handles Requests Asynchronously
        /// </summary>
        /// <param name="context">Injected <see cref="HttpContext"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task InvokeAsync(HttpContext @context)
        {
            @context.Response.Headers.Remove("X-Powered-By");
            @context.Response.Headers.Remove("Server");
            @context.Response.Headers.XContentTypeOptions = new StringValues("nosniff");
            @context.Response.Headers.XFrameOptions = new StringValues("SAMEORIGIN");
            @context.Response.Headers.XXSSProtection = new StringValues("1; mode=block");
            @context.Response.Headers.ContentSecurityPolicy = new StringValues("default-src: https:; frame-ancestors 'self' X-Frame-Options: SAMEORIGIN");
            @context.Response.Headers.StrictTransportSecurity = new StringValues("max-age=16070400; includeSubDomains; preload");

            await request(@context);
        }
    }
}
