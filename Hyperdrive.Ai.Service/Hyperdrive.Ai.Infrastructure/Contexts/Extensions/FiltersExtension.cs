using Hyperdrive.Ai.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Ai.Infrastructure.Contexts.Extensions;

/// <summary>
///     Represents a <see cref="FiltersExtension" /> class.
/// </summary>
public static class FiltersExtension
{

    public const string SoftDeleteFilter = nameof(SoftDeleteFilter);

    /// <summary>
    ///     Extends Customized Filters
    /// </summary>
    /// <param name="this">Injected <see cref="ModelBuilder" /></param>
    public static void AddCustomizedFilters(this ModelBuilder @this)
    {
        // Configure entity filters           
        @this.Entity<Document>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<Chunk>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<Chat>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
        @this.Entity<Message>().HasQueryFilter(SoftDeleteFilter, p => !p.Deleted);
    }
}