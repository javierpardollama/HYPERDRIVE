using Hyperdrive.Intelligence.Domain.Entities;
using Hyperdrive.Intelligence.Infrastructure.Contexts.Extensions;
using Hyperdrive.Intelligence.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Intelligence.Infrastructure.Contexts;

/// <summary>
///     Represents a <see cref="ApplicationContext" /> class. Inherits <see cref="DbContext" />. Implements
///     <see cref="IApplicationContext" />
/// </summary>
/// <param name="options">Injected <see cref="DbContextOptions{ApplicationContext}" /></param>
public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options), IApplicationContext
{
    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Document}" />
    /// </summary>
    public virtual DbSet<Document> Document { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Chunk}" />
    /// </summary>
    public virtual DbSet<Chunk> Chunk { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Chat}" />
    /// </summary>
    public virtual DbSet<Chat> Chat { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Interaction}" />
    /// </summary>
    public virtual DbSet<Interaction> Interactions { get; set; }

    /// <summary>
    ///     Overrides Model Creation
    /// </summary>
    /// <param name="modelBuilder">Injected <see cref="ModelBuilder" /></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddCustomizedCollections();
        modelBuilder.AddCustomizedFilters();
    }
}