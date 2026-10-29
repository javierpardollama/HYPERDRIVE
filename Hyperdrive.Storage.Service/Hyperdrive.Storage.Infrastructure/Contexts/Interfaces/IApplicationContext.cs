using Hyperdrive.Storage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Storage.Infrastructure.Contexts.Interfaces;

/// <summary>
/// Represents a <see cref="IApplicationContext"/> interface. Inherits <see cref="IDisposable"/>.
/// </summary>
public interface IApplicationContext : IDisposable
{
    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUser}"/>
    /// </summary>
    DbSet<ApplicationUser> Users { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItem}"/>
    /// </summary>
    DbSet<DriveItem> DriveItems { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserDriveItem}"/>
    /// </summary>
    DbSet<ApplicationUserDriveItem> ApplicationUserDriveItems { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItemInfo}"/>
    /// </summary>
    DbSet<DriveItemInfo> DriveItemInfos { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItemContent}"/>
    /// </summary>
    DbSet<DriveItemContent> DriveItemContents { get; set; }

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
