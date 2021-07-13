using Hyperdrive.Tier.Settings.Classes;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Tier.Web.Extensions
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
        public static void AddCustomizedCrossOriginRequests(this IServiceCollection @this, JwtSettings JwtSettings)
        {
            @this.AddCors(options =>
            {
                options.AddPolicy("Authentication", builder =>
                {
                    builder.WithOrigins(JwtSettings.JwtAudience).AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });
        }
    }
}
