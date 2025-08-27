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
            cfg.RegisterServicesFromAssemblyContaining(typeof(AddApplicationRoleHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(FindAllApplicationRoleHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(FindPaginatedApplicationRoleHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(RemoveApplicationRoleByIdHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(UpdateApplicationRoleHandler));
                    
            cfg.RegisterServicesFromAssemblyContaining(typeof(FindAllApplicationUserHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(FindPaginatedApplicationUserHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(RemoveApplicationUserByIdHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(UpdateApplicationUserHandler));
                    
            cfg.RegisterServicesFromAssemblyContaining(typeof(JoinInHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(SignInHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(SignOutHandler));
            
            cfg.RegisterServicesFromAssemblyContaining(typeof(AddDriveItemHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(FindAllDriveItemVersionByDriveItemIdHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(FindPaginatedDriveItemByApplicationUserIdHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(FindPaginatedSharedDriveItemByApplicationUserIdHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(RemoveDriveItemByIdHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(UpdateDriveItemHandler));
                
            
            cfg.RegisterServicesFromAssemblyContaining(typeof(EmailChangeHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(PasswordChangeHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(NameChangeHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(PasswordResetHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(PhoneNumberChangeHandler));
            cfg.RegisterServicesFromAssemblyContaining(typeof(RefreshTokenResetHandler));
        });
    }
}