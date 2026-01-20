using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Middlewares;

/// <summary>
///     Represents a <see cref="HeaderMiddleware" /> class
/// </summary>
/// <param name="request">Injected <see cref="RequestDelegate" /></param>
public class HeaderMiddleware(RequestDelegate @request)
{
    /// <summary>
    ///     Handles Requests Asynchronously
    /// </summary>
    /// <param name="context">Injected <see cref="HttpContext" /></param>
    /// <returns>Instance of <see cref="Task" /></returns>
    public async Task InvokeAsync(HttpContext @context)
    {
        @context.Response.Headers.Remove("X-Powered-By");
        @context.Response.Headers.Remove("Server");
        @context.Response.Headers.Remove("x-aspnet-version");
        @context.Response.Headers.XContentTypeOptions = new StringValues("nosniff");
        @context.Response.Headers.XFrameOptions = new StringValues("DENY");
        @context.Response.Headers.XXSSProtection = new StringValues("0");
        @context.Response.Headers.ContentSecurityPolicy = new StringValues("default-src 'self'");
        @context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        @context.Response.Headers.Append("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");

        @context.Response.Headers.Append("X-Clacks-Overhead", new StringValues("GNU Terry Pratchett"));

        await @request(@context);
    }
}