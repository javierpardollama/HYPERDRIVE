using Hyperdrive.Tier.Contexts.Extensions;
using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Contexts.Classes
{
    /// <summary>
    /// Represents a <see cref="ApplicationContext"/> class. Inherits <see cref="DbContext"/>. Implements <see cref="IApplicationContext"/>. Inherits <see cref="IdentityDbContext"/>
    /// </summary>
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IApplicationContext
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="ApplicationContext"/>
        /// </summary>
        /// <param name="options">Injected <see cref="DbContextOptions{ApplicationContext}"/></param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

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
        /// Gets or Sets <see cref="DbSet{Archive}"/>
        /// </summary>
        public virtual DbSet<Archive> Archive { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ApplicationUserArchive}"/>
        /// </summary>
        public virtual DbSet<ApplicationUserArchive> ApplicationUserArchive { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="DbSet{ArchiveVersion}"/>
        /// </summary>
        public virtual DbSet<ArchiveVersion> ArchiveVersion { get; set; }

        /// <summary>
        /// Saves Changes Syncronously
        /// </summary>
        /// <returns>Instance of <see cref="int"/></returns>
        public override int SaveChanges()
        {
            UpdateSoftStatus();
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves Changes Asyncronously
        /// </summary>
        /// <returns>Instance of <see cref="Task{int}"/></returns>
        public async Task<int> SaveChangesAsync()
        {
            UpdateSoftStatus();
            return await base.SaveChangesAsync();
        }

        /// <summary>
        /// Overrides Tracking
        /// </summary>
        private void UpdateSoftStatus()
        {
            foreach (EntityEntry entity in ChangeTracker.Entries())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.CurrentValues["LastModified"] = DateTime.Now;
                        entity.State = EntityState.Added;
                        entity.CurrentValues["Deleted"] = false;
                        break;

                    case EntityState.Modified:
                        entity.CurrentValues["LastModified"] = DateTime.Now;
                        entity.State = EntityState.Modified;
                        entity.CurrentValues["Deleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entity.CurrentValues["LastModified"] = DateTime.Now;
                        entity.State = EntityState.Modified;
                        entity.CurrentValues["Deleted"] = true;
                        break;
                }
            }
        }

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
