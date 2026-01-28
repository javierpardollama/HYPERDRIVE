using Hyperdrive.Ai.Domain.Managers;
using OpenAI.Embeddings;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="EmbeddingManager" /> class. Implements <see cref="IEmbeddingManager" />
/// </summary>
public class EmbeddingManager : IEmbeddingManager
{
    private readonly EmbeddingClient Client;

    /// <summary>
    ///     Initializes a new Instance of <see cref="EmbeddingManager" />
    /// </summary>
    /// <param name="client"> Injected <see cref="EmbeddingClient"/></param>
    public EmbeddingManager(EmbeddingClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Gets Embedding
    /// </summary>
    /// <param name="text"></param>
    /// <param name="ct"></param>
    /// <returns>Instance of <see cref="Task{float[]}"/></returns>
    public async Task<float[]> GetEmbedding(string text, CancellationToken ct = default)
    {
        var result = await Client.GenerateEmbeddingAsync(text, cancellationToken: ct);

        return result.Value.ToFloats().ToArray();
    }

    /// <summary>
    /// Gets Embeddings
    /// </summary>
    /// <param name="texts"></param>
    /// <param name="ct"></param>
    /// <returns>Instance of <see cref="Task{IReadOnlyList{float[]}}"/></returns>
    public async Task<IReadOnlyList<float[]>> GetEmbeddings(IReadOnlyList<string> texts, CancellationToken ct = default)
    {
        var result = await Client.GenerateEmbeddingsAsync(texts, cancellationToken: ct);

        return [.. result.Value.Select(r => r.ToFloats().ToArray())];
    }
}
