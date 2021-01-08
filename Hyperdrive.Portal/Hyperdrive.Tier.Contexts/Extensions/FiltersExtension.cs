using Hyperdrive.Tier.Entities.Classes;

using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Tier.Contexts.Extensions
{
    /// <summary>
    /// Represents a <see cref="FiltersExtension"/> class.
    /// </summary>
    public static class FiltersExtension
    {
        /// <summary>
        /// Extends Customized Filters
        /// </summary>
        /// <param name="this">Injected <see cref="ModelBuilder"/></param>
        public static void AddCustomizedFilters(this ModelBuilder @this)
        {
            // Configure entity filters      
            @this.Entity<ApplicationRole>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ApplicationRoleClaim>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ApplicationUser>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ApplicationUserClaim>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ApplicationUserLogin>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ApplicationUserRole>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ApplicationUserToken>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<Archive>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ApplicationUserArchive>().HasQueryFilter(p => !p.Deleted);
            @this.Entity<ArchiveVersion>().HasQueryFilter(p => !p.Deleted);

        }
    }
}
