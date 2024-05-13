using System;
using System.Threading.RateLimiting;

using Hyperdrive.Tier.Settings.Classes;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="RateLimitExtension"/> class.
    /// </summary>
    public static class RateLimitExtension
    {
        /// <summary>
        /// Extends Customized Rate Limit
        /// </summary>
        /// <param name="this">Injected <see cref="IServiceCollection"/></param>
        /// <param name="RateSettings">Injected <see cref="RateLimitSettings"/></param>
        public static void AddCustomizedRateLimiter(this IServiceCollection @this, RateLimitSettings @RateSettings)
        {
            @this.AddRateLimiter(_ => _
             .AddFixedWindowLimiter(policyName: @RateSettings.PolicyName, options =>
             {
                 options.PermitLimit = @RateSettings.PermitLimit;
                 options.Window = TimeSpan.FromSeconds(@RateSettings.Window);
                 options.QueueProcessingOrder = (QueueProcessingOrder)@RateSettings.QueueProcessingOrder;
                 options.QueueLimit = @RateSettings.QueueLimit;
             }));
        }
    }
}
