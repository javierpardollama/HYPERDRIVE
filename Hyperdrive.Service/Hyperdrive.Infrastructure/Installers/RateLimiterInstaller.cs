using System.Threading.RateLimiting;
using Hyperdrive.Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="RateLimiterInstaller" /> class.
/// </summary>
public static class RateLimiterInstaller
{
    /// <summary>
    ///     Extends Customized Rate Limit
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="RateLimitSettings" /></param>
    public static void InstallRateLimiter(this IServiceCollection @this, RateLimitSettings @settings)
    {
        @this.AddRateLimiter(_ => _
            .AddConcurrencyLimiter(settings.PolicyName, options =>
            {
                options.PermitLimit = settings.PermitLimit;
                options.QueueProcessingOrder = (QueueProcessingOrder)settings.QueueProcessingOrder;
                options.QueueLimit = settings.QueueLimit;
            }));
    }
}