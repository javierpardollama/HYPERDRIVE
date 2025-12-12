using Hyperdrive.Application.Handlers.ApplicationRole;
using Hyperdrive.Application.Handlers.ApplicationUser;
using Hyperdrive.Application.Handlers.Auth;
using Hyperdrive.Application.Handlers.DriveItem;
using Hyperdrive.Application.Handlers.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Application.Installers;

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
            cfg.RegisterServicesFromAssemblyContaining<AddApplicationRoleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindAllApplicationRoleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedApplicationRoleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RemoveApplicationRoleByIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateApplicationRoleHandler>();
                    
            cfg.RegisterServicesFromAssemblyContaining<FindAllApplicationUserHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedApplicationUserHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RemoveApplicationUserByIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateApplicationUserHandler>();
                    
            cfg.RegisterServicesFromAssemblyContaining<JoinInHandler>();
            cfg.RegisterServicesFromAssemblyContaining<SignInHandler>();
            cfg.RegisterServicesFromAssemblyContaining<SignOutHandler>();
            
            cfg.RegisterServicesFromAssemblyContaining<AddDriveItemHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedDriveItemVersionByDriveItemIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedDriveItemByApplicationUserIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindPaginatedSharedDriveItemByApplicationUserIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RemoveDriveItemByIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateDriveItemNameHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindLatestDriveItemBinaryByIdHandler>();
            cfg.RegisterServicesFromAssemblyContaining<FindDriveItemBinaryByIdHandler>();

            cfg.RegisterServicesFromAssemblyContaining<EmailChangeHandler>();
            cfg.RegisterServicesFromAssemblyContaining<PasswordChangeHandler>();
            cfg.RegisterServicesFromAssemblyContaining<NameChangeHandler>();
            cfg.RegisterServicesFromAssemblyContaining<PasswordResetHandler>();
            cfg.RegisterServicesFromAssemblyContaining<PhoneNumberChangeHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RefreshTokenResetHandler>();
        });
    }
}