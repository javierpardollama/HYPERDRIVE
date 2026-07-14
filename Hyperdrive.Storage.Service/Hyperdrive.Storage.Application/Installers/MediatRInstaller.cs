using Hyperdrive.Storage.Application.Handlers.DriveItem;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Storage.Application.Installers;

/// <summary>
///     Represents a <see cref="InstallMediatR" /> class.
/// </summary>
public static class MediatRInstaller
{
    /// <summary>
    ///     Installs MediatR
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallMediatR(this IServiceCollection @this)
    {
        @this.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<AddDriveItemHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedDriveItemVersionByDriveItemIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedDriveItemByApplicationUserIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedSharedDriveItemByApplicationUserIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RemoveDriveItemByIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateDriveItemNameHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindLatestDriveItemBinaryByIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindDriveItemBinaryByIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<TargetDriveItemVersionByIdHandler>();
        });
    }
}