using Hyperdrive.Identity.Domain.Managers;
using Hyperdrive.Identity.Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Identity.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="ManagerInstaller" /> class.
/// </summary>
public static class ManagerInstaller
{
    /// <summary>
    ///     Installs Managers
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallManagers(this IServiceCollection @this)
    {
        @this.AddTransient<IApplicationRoleManager, ApplicationRoleManager>();
        @this.AddTransient<IApplicationUserManager, ApplicationUserManager>();
        @this.AddTransient<IAuthManager, AuthManager>();
        @this.AddTransient<ITokenManager, TokenManager>();
        @this.AddTransient<IRefreshTokenManager, RefreshTokenManager>();
        @this.AddTransient<ITokenManager, TokenManager>();
        @this.AddTransient<ISecurityManager, SecurityManager>();
        // Add other managers here
    }
}