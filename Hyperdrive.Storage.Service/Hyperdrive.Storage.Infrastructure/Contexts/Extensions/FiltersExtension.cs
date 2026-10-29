using Hyperdrive.Storage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Storage.Infrastructure.Contexts.Extensions;

/// <summary>
/// Represents a <see cref="FiltersExtension"/> class.
/// </summary>
public static class FiltersExtension
{
    public const string SoftDeleteFilter = nameof(SoftDeleteFilter);

    /// <summary>
    /// Extends Customized Filters
    /// </summary>
    /// <param name="this">Injected <see cref="ModelBuilder"/></param>
    public static void AddCustomizedFilters(this ModelBuilder @this)
    {
        // Configure entity filters    
        @this.Entity<DriveItem>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUserDriveItem>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<DriveItemInfo>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<DriveItemContent>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
    }
}
