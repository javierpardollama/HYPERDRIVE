using System;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;

using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Tier.Contexts.Interfaces
{
    public interface IApplicationContext : IDisposable
    {
        DbSet<ApplicationRole> ApplicationRole { get; set; }

        DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }

        DbSet<ApplicationUser> ApplicationUser { get; set; }

        DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }

        DbSet<ApplicationUserLogin> ApplicationUserLogin { get; set; }

        DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }

        DbSet<ApplicationUserToken> ApplicationUserToken { get; set; }

        DbSet<Archive> Archive { get; set; }

        DbSet<ApplicationUserArchive> ApplicationUserArchive { get; set; }

        DbSet<ArchiveVersion> ArchiveVersion { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
