using Hyperdrive.Storage.Domain.Entities;
using Hyperdrive.Storage.Infrastructure.Contexts.Extensions;
using Hyperdrive.Storage.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Storage.Infrastructure.Contexts;

/// <summary>
/// Represents a <see cref="ApplicationContext"/> class. Inherits <see cref="DbContext"/>. Implements <see cref="IApplicationContext"/>.
/// </summary>    
/// <param name="options">Injected <see cref="DbContextOptions{ApplicationContext}"/></param>
public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options), IApplicationContext
{
 
    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUser}"/>
    /// </summary>
    public virtual DbSet<ApplicationUser> Users { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItem}"/>
    /// </summary>
    public virtual DbSet<DriveItem> DriveItems { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{ApplicationUserDriveItem}"/>
    /// </summary>
    public virtual DbSet<ApplicationUserDriveItem> ApplicationUserDriveItems { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItemInfo}"/>
    /// </summary>
    public virtual DbSet<DriveItemInfo> DriveItemInfos { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="DbSet{DriveItemContent}"/>
    /// </summary>
    public virtual DbSet<DriveItemContent> DriveItemContents { get; set; }

    /// <summary>
    /// Overrides Model Creation
    /// </summary>
    /// <param name="modelBuilder">Injected <see cref="ModelBuilder"/></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddCustomizedFilters();
    }
}
