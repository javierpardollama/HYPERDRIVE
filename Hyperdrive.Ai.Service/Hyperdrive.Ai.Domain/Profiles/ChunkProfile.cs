using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ChunkProfile"/> class.
/// </summary>
public static class ChunkProfile
{
    /// <summary>
    /// Transforms to Rag Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Chunk"/></param>
    /// <returns>Instance of <see cref="RagSourceDto"/></returns>
    public static RagSourceDto ToRagDto(this Chunk @entity)
    {
        return new RagSourceDto()
        {
            DocumentId = @entity.DocumentId,
            Preview = @entity.Text.Length > 240 ? @entity.Text[..240] + "…" : @entity.Text
        };
    }

}
