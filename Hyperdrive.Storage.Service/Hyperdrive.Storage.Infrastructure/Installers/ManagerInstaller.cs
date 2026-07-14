using Hyperdrive.Storage.Domain.Managers;
using Hyperdrive.Storage.Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Storage.Infrastructure.Installers;

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

        @this.AddTransient<IApplicationUserManager, ApplicationUserManager>();
        @this.AddTransient<IDriveItemManager, DriveItemManager>();
        @this.AddTransient<IDriveItemVersionManager, DriveItemVersionManager>();
        @this.AddTransient<IDriveItemBinaryManager, DriveItemBinaryManager>();
        @this.AddTransient<IDriveItemInfoManager, DriveItemInfoManager>();
        @this.AddTransient<IDriveItemContentManager, DriveItemContentManager>();
        // Add other managers here
    }
}