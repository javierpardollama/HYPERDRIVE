using Hyperdrive.Ai.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Hyperdrive.Ai.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ChunkProfile"/> class.
/// </summary>
public static class ChunkProfile
{
    /// <summary>
    /// Transforms to Source
    /// </summary>
    /// <param name="entity">Injected <see cref="Chunk"/></param>
    /// <returns>Instance of <see cref="Source"/></returns>
    public static Source ToSource(this Chunk @entity)
    {
        return new Source()
        {
            DocumentId = @entity.DocumentId,
            Preview = @entity.Text.Length > 240 ? @entity.Text[..240] + "…" : @entity.Text
        };
    }

    /// <summary>
    /// Transforms to Context
    /// </summary>
    /// <param name="entities">Injected <see cref="ICollection{Chunk}>"/></param>
    /// <returns>Instance of <see cref="string"/></returns>
    public static string ToContext(this ICollection<Chunk> @entities)
    {
        return string.Join("\n\n---\n\n",
              @entities.Select((c, i) => $"[Chunk {i + 1}] {c.Text}")); ;
    }

}
