using Hyperdrive.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Infrastructure.Contexts.Extensions
{
    /// <summary>
    /// Represents a <see cref="IdentitiesExtension"/> class.
    /// </summary>
    public static class IdentitiesExtension
    {
        /// <summary>
        /// Extends Customized Identities
        /// </summary>
        /// <param name="this">Injected <see cref="ModelBuilder"/></param>
        public static void AddCustomizedIdentities(this ModelBuilder @this)
        {
            @this.Entity<ApplicationUserRole>(applicationUserRole =>
            {
                applicationUserRole.ToTable("ApplicationUserRole");

                applicationUserRole.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.ApplicationUserRoles)
                .HasForeignKey(x => x.UserId);

                applicationUserRole.HasOne(x => x.ApplicationRole)
                .WithMany(x => x.ApplicationUserRoles)
                .HasForeignKey(x => x.RoleId);
            });

            @this.Entity<ApplicationRoleClaim>(applicationRoleClaim =>
            {
                applicationRoleClaim.ToTable("ApplicationRoleClaim");

                applicationRoleClaim.HasOne(x => x.ApplicationRole)
                .WithMany(x => x.ApplicationRoleClaims)
                .HasForeignKey(x => x.RoleId);
            });

            @this.Entity<ApplicationUserClaim>(applicationUserClaim =>
            {
                applicationUserClaim.ToTable("ApplicationUserClaim");

                applicationUserClaim.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.ApplicationUserClaims)
                .HasForeignKey(x => x.UserId);
            });

            @this.Entity<ApplicationUserLogin>(applicationUserLogin =>
            {
                applicationUserLogin.ToTable("ApplicationUserLogin");

                applicationUserLogin.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.ApplicationUserLogins)
                .HasForeignKey(x => x.UserId);

            });

            @this.Entity<ApplicationUserToken>(applicationUserToken =>
            {
                applicationUserToken.ToTable("ApplicationUserToken");

                applicationUserToken.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.ApplicationUserTokens)
                .HasForeignKey(x => x.UserId);
            });

            @this.Entity<ApplicationUser>(applicationUser =>
            {
                applicationUser.ToTable("ApplicationUser");

                applicationUser.HasIndex(p => new { p.NormalizedEmail, p.Deleted })
                .IsUnique(false);

                applicationUser.HasIndex(p => new { p.NormalizedUserName, p.Deleted })
               .IsUnique(false);
            });

            @this.Entity<ApplicationRole>(applicationRole =>
            {
                applicationRole.ToTable("ApplicationRole");

                applicationRole.HasIndex(p => new { p.NormalizedName, p.Deleted })
               .IsUnique(false);
            });
        }
    }
}
