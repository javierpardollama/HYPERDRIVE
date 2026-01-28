using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IChunkManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IChunkManager : IBaseManager
{
    /// <summary>
    /// Adds Chunks
    /// </summary>
    /// <param name="documentid">Injected <see cref="Guid"/></param>
    /// <param name="filecontent">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public Task AddChunks(Guid @documentid,
                          string @filecontent);

    /// <summary>
    /// Finds Chunks By Text
    /// </summary>
    /// <param name="text">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{ICollection{Entities.Chunk}}"/></returns>
    public Task<ICollection<Entities.Chunk>> FindByText(string text);
}
