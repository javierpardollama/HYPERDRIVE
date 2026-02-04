using Hyperdrive.Main.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Main.Infrastructure.Contexts.Extensions;

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
        @this.Entity<ApplicationRole>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationRoleClaim>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUser>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUserClaim>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUserLogin>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUserRole>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUserToken>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<DriveItem>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUserDriveItem>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<DriveItemInfo>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<DriveItemContent>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<ApplicationUserRefreshToken>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);

    }
}
