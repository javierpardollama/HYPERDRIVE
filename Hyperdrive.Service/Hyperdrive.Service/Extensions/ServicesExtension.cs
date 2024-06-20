using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Tier.Service.Extensions
{
    /// <summary>
    /// Represents a <see cref="ServicesExtension"/> class.
    /// </summary>
    public static class ServicesExtension
    {
        /// <summary>
        /// Extends Customized Services
        /// </summary>
        /// <param name="this">Injected <see cref="IServiceCollection"/></param>
        public static void AddCustomizedServices(this IServiceCollection @this)
        {
            @this.AddTransient<ITokenService, TokenService>();
            @this.AddTransient<IAuthService, AuthService>();
            @this.AddTransient<ISecurityService, SecurityService>();
            @this.AddTransient<IDriveItemService, DriveItemService>();
            @this.AddTransient<IApplicationRoleService, ApplicationRoleService>();
            @this.AddTransient<IApplicationUserService, ApplicationUserService>();

            // Add other services here
        }
    }
}
