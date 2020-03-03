using System;
using System.Threading.Tasks;

using Hyperdrive.Tier.Contexts.Extensions;
using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hyperdrive.Tier.Contexts.Classes
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public virtual DbSet<ApplicationRole> ApplicationRole { get; set; }

        public virtual DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }

        public virtual DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }

        public virtual DbSet<ApplicationUserLogin> ApplicationUserLogin { get; set; }

        public virtual DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }

        public virtual DbSet<ApplicationUserToken> ApplicationUserToken { get; set; }

        public virtual DbSet<Archive> Archive { get; set; }

        public virtual DbSet<ApplicationUserArchive> ApplicationUserArchive { get; set; }

        public virtual DbSet<ArchiveVersion> ArchiveVersion { get; set; }

        public override int SaveChanges()
        {
            UpdateSoftStatus();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            UpdateSoftStatus();
            return await base.SaveChangesAsync();
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddCustomizedIdentities();

            modelBuilder.AddCustomizedFilters();
        }
    }
}
