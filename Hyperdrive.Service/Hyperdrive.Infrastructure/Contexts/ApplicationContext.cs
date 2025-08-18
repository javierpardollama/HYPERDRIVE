using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Contexts.Extensions;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Infrastructure.Contexts
{
    /// <summary>
    /// Represents a <see cref="ApplicationContext"/> class. Inherits <see cref="DbContext"/>. Implements <see cref="IdentityDbContext"/>. Inherits <see cref="options"/>
    /// </summary>    
    /// <param name="options">Injected <see cref="IApplicationContext"/></param>
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>(options), IApplicationContext
    {
        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationRole}"/>
        /// </summary>
        public override DbSet<ApplicationRole> Roles { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationRoleClaim}"/>
        /// </summary>
        public override DbSet<ApplicationRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUser}"/>
        /// </summary>
        public override DbSet<ApplicationUser> Users { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUserClaim}"/>
        /// </summary>
        public override DbSet<ApplicationUserClaim> UserClaims { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUserLogin}"/>
        /// </summary>
        public override DbSet<ApplicationUserLogin> UserLogins { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUserRole}"/>
        /// </summary>
        public override DbSet<ApplicationUserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUserToken}"/>
        /// </summary>
        public override DbSet<ApplicationUserToken> UserTokens { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUserToken}"/>
        /// </summary>
        public virtual DbSet<ApplicationUserRefreshToken> UserRefreshTokens { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{DriveItem}"/>
        /// </summary>
        public virtual DbSet<DriveItem> DriveItems { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUserDriveItem}"/>
        /// </summary>
        public virtual DbSet<ApplicationUserDriveItem> ApplicationUserDriveItems { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{DriveItemVersion}"/>
        /// </summary>
        public virtual DbSet<DriveItemVersion> DriveItemVersions { get; set; }

        /// <summary>
        /// Overrides Model Creation
        /// </summary>
        /// <param name="modelBuilder">Injected <see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddCustomizedIdentities();

            modelBuilder.AddCustomizedFilters();
        }
    }
}
