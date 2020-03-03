using Hyperdrive.Tier.Services.Classes;
using Hyperdrive.Tier.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Tier.Web.Extensions
{
    public static class ServicesExtension
    {
        public static void AddCustomizedServices(this IServiceCollection @this)
        {
            @this.AddTransient<ITokenService, TokenService>();
            @this.AddTransient<IAuthService, AuthService>();
            @this.AddTransient<ISecurityService, SecurityService>();
            @this.AddTransient<IArchiveService, ArchiveService>();

            // Add other services here
        }
    }
}
