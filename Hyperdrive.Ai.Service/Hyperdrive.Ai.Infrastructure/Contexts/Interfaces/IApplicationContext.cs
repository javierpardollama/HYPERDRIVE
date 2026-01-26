using Hyperdrive.Ai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;

/// <summary>
///     Represents a <see cref="IApplicationContext" /> interface. Inherits <see cref="IDisposable" />.
/// </summary>
public interface IApplicationContext : IDisposable
{
    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Chunk}" />
    /// </summary>
    public DbSet<Chunk> Chunk { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Document}" />
    /// </summary>
    public DbSet<Document> Document { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Document}" />
    /// </summary>
    public DbSet<Chat> Chat { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DbSet{Message}" />
    /// </summary>
    public DbSet<Interaction> Message { get; set; }

    /// <summary>
    ///     Saves Changes Synchronously
    /// </summary>
    /// <returns>Instance of <see cref="int" /></returns>
    public int SaveChanges();

    /// <summary>
    ///     Saves Changes Asynchronously
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken" /></param>
    /// <returns>Instance of <see cref="Task{int}" /></returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}