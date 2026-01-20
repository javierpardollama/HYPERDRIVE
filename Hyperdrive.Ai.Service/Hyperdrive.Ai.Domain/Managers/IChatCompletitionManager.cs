using Hyperdrive.Ai.Domain.Dtos;
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
    /// <param name="text">Injected <see cref="string"/></param>
    /// <param name="chunks">Injected <see cref="List{Entities.Chunk}"/></param>
    /// <returns>Instance of <see cref="RagAnswerDto"/></returns>
    public Task<RagAnswerDto> GetCompletionAsync(string text, List<Entities.Chunk> chunks);
}
