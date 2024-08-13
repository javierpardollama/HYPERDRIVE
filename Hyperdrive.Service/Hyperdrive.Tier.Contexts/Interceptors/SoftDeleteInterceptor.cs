﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sandwitch.Tier.Contexts.Interceptors
{
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
            foreach (EntityEntry @entity in eventData.Context.ChangeTracker.Entries())
            {
                switch (@entity.State)
                {
                    case EntityState.Added:
                        @entity.CurrentValues["LastModified"] = DateTime.Now;
                        @entity.State = EntityState.Added;
                        @entity.CurrentValues["Deleted"] = false;
                        break;

                    case EntityState.Modified:
                        @entity.CurrentValues["LastModified"] = DateTime.Now;
                        @entity.State = EntityState.Modified;
                        @entity.CurrentValues["Deleted"] = false;
                        break;

                    case EntityState.Deleted:
                        @entity.CurrentValues["LastModified"] = DateTime.Now;
                        @entity.State = EntityState.Modified;
                        @entity.CurrentValues["Deleted"] = true;
                        break;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Overrides Saving Changes
        /// </summary>
        /// <param name="eventData">Injected <see cref="DbContextEventData"/></param>
        /// <param name="result">Injected <see cref="intInterceptionResult{}"/></param>
        /// <returns>Instance of <see cref="InterceptionResult{int}"/></returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            foreach (EntityEntry @entity in eventData.Context.ChangeTracker.Entries())
            {
                switch (@entity.State)
                {
                    case EntityState.Added:
                        @entity.CurrentValues["LastModified"] = DateTime.Now;
                        @entity.State = EntityState.Added;
                        @entity.CurrentValues["Deleted"] = false;
                        break;

                    case EntityState.Modified:
                        @entity.CurrentValues["LastModified"] = DateTime.Now;
                        @entity.State = EntityState.Modified;
                        @entity.CurrentValues["Deleted"] = false;
                        break;

                    case EntityState.Deleted:
                        @entity.CurrentValues["LastModified"] = DateTime.Now;
                        @entity.State = EntityState.Modified;
                        @entity.CurrentValues["Deleted"] = true;
                        break;
                }
            }

            return base.SavingChanges(eventData, result);
        }
    }
}
