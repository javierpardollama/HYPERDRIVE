using Hyperdrive.Ai.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IChatCompletitionManager"/> interface.
/// </summary>
public interface IChatCompletitionManager
{
    /// <summary>
    /// Gets Chat Completition
    /// </summary>
    /// <param name="query">Injected <see cref="Query"/></param>
    /// <param name="chunks">Injected <see cref="List{Entities.Chunk}"/></param>
    /// <returns>Instance of <see cref="Answer"/></returns>
    public Task<Answer> GetCompletionAsync(Query query, List<Entities.Chunk> chunks);
}
