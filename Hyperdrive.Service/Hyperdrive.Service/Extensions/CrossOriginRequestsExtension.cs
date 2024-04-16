using Hyperdrive.Tier.Settings.Classes;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Tier.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="CrossOriginRequestsExtension"/> class.
    /// </summary>
    public static class CrossOriginRequestsExtension
    {
        /// <summary>
        /// Extends Customized Cross Origin Requests
        /// </summary>
        /// <param name="this">Injected <see cref="IServiceCollection"/></param>
        /// <param name="JwtSettings">Injected <see cref="JwtSettings"/></param>
        public static void AddCustomizedCrossOriginRequests(this IServiceCollection @this, JwtSettings @JwtSettings)
        {
            @this.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins([.. @JwtSettings.JwtAudiences])
                                                                  .AllowCredentials()
                                                                  .AllowAnyMethod()
                                                                  .AllowAnyHeader()
                                                                  .Build();
                });
            });
        }
    }
}
