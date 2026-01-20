using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IEmbeddingManager"/> interface.
/// </summary>
public interface IEmbeddingManager
{
    /// <summary>
    /// Gets Embedding
    /// </summary>
    /// <param name="text"></param>
    /// <param name="ct"></param>
    /// <returns>Instance of <see cref="float[]"/></returns>
    Task<float[]> GetEmbedding(string text, CancellationToken ct = default);

    /// <summary>
    /// Gets Embeddings
    /// </summary>
    /// <param name="texts"></param>
    /// <param name="ct"></param>
    /// <returns>Instance of <see cref="{IReadOnlyList{float[]}"/></returns>
    Task<IReadOnlyList<float[]>> GetEmbeddings(IReadOnlyList<string> texts, CancellationToken ct = default);
}
