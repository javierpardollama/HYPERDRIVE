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
    /// <param name="interaction">Injected <see cref="Entities.Interaction"/></param>
    /// <param name="chunks">Injected <see cref="ICollection{Entities.Chunk}"/></param>
    /// <returns>Instance of <see cref="Entities.Interaction"/></returns>
    public Task<Entities.Interaction> GetCompletionAsync(Entities.Interaction @interaction, ICollection<Entities.Chunk> @chunks);
}
