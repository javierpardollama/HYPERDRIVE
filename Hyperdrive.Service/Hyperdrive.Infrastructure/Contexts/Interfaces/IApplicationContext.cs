using Hyperdrive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Infrastructure.Contexts.Interfaces;

/// <summary>
/// Represents a <see cref="IApplicationContext"/> interface. Inherits <see cref="IDisposable"/>.
/// </summary>
public interface IApplicationContext : IDisposable
{
    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationRole}"/>
    /// </summary>
    DbSet<ApplicationRole> Roles { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationRoleClaim}"/>
    /// </summary>
    DbSet<ApplicationRoleClaim> RoleClaims { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUser}"/>
    /// </summary>
    DbSet<ApplicationUser> Users { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserClaim}"/>
    /// </summary>
    DbSet<ApplicationUserClaim> UserClaims { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserLogin}"/>
    /// </summary>
    DbSet<ApplicationUserLogin> UserLogins { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserRole}"/>
    /// </summary>
    DbSet<ApplicationUserRole> UserRoles { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserToken}"/>
    /// </summary>
    DbSet<ApplicationUserToken> UserTokens { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserToken}"/>
    /// </summary>
    DbSet<ApplicationUserRefreshToken> UserRefreshTokens { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItem}"/>
    /// </summary>
    DbSet<DriveItem> DriveItems { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserDriveItem}"/>
    /// </summary>
    DbSet<ApplicationUserDriveItem> ApplicationUserDriveItems { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItemVersion}"/>
    /// </summary>
    DbSet<DriveItemVersion> DriveItemVersions { get; set; }

    /// <summary>
    /// Saves Changes Syncronously
    /// </summary>
    /// <returns>Instance of <see cref="int"/></returns>
    int SaveChanges();

    /// <summary>
    /// Saves Changes Asyncronously
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task{int}"/></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
