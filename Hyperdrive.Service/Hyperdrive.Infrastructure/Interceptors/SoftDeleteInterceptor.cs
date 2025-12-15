using System;
using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Hyperdrive.Infrastructure.Interceptors;

/// <summary>
/// Represents a <see cref="SoftDeleteInterceptor"/> class. Inherits <see cref="SaveChangesInterceptor"/>
/// </summary>
public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    /// <summary>
    /// Overrides Saving Changes Async
    /// </summary>
    /// <param name="eventData">Injected <see cref="DbContextEventData"/></param>
    /// <param name="result">Injected <see cref="InterceptionResult{}"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="ValueTask{InterceptionResult{int}}"/></returns>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        foreach (EntityEntry @entity in eventData.Context.ChangeTracker.Entries<IBase>())
        {
            switch (@entity.State)
            {
                case EntityState.Added:
                    @entity.Property(nameof(IBase.CreatedAt)).CurrentValue = DateTime.UtcNow;
                    @entity.State = EntityState.Added;
                    @entity.Property(nameof(IBase.Deleted)).CurrentValue = false;
                    break;

                case EntityState.Modified:
                    @entity.Property(nameof(IBase.ModifiedAt)).CurrentValue = DateTime.UtcNow;
                    @entity.State = EntityState.Modified;
                    @entity.Property(nameof(IBase.Deleted)).CurrentValue = false;
                    break;

                case EntityState.Deleted:
                    @entity.Property(nameof(IBase.DeletedAt)).CurrentValue = DateTime.UtcNow;
                    @entity.State = EntityState.Modified;
                    @entity.Property(nameof(IBase.Deleted)).CurrentValue = true;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// Overrides Saving Changes
    /// </summary>
    /// <param name="eventData">Injected <see cref="DbContextEventData"/></param>
    /// <param name="result">Injected <see cref="InterceptionResult{int}"/></param>
    /// <returns>Instance of <see cref="InterceptionResult{int}"/></returns>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        foreach (EntityEntry @entity in eventData.Context.ChangeTracker.Entries<IBase>())
        {
            switch (@entity.State)
            {
                case EntityState.Added:
                    @entity.Property(nameof(IBase.CreatedAt)).CurrentValue = DateTime.UtcNow;
                    @entity.State = EntityState.Added;
                    @entity.Property(nameof(IBase.Deleted)).CurrentValue = false;
                    break;

                case EntityState.Modified:
                    @entity.Property(nameof(IBase.ModifiedAt)).CurrentValue = DateTime.UtcNow;
                    @entity.State = EntityState.Modified;
                    @entity.Property(nameof(IBase.Deleted)).CurrentValue = false;
                    break;

                case EntityState.Deleted:
                    @entity.Property(nameof(IBase.DeletedAt)).CurrentValue = DateTime.UtcNow;
                    @entity.State = EntityState.Modified;
                    @entity.Property(nameof(IBase.Deleted)).CurrentValue = true;
                    break;
            }
        }

        return base.SavingChanges(eventData, result);
    }
}
