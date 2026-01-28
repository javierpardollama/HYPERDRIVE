using Hyperdrive.Ai.Domain.Entities;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;
using Hyperdrive.Ai.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="ChunkManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IChunkManager"/>
/// </summary>
public class ChunkManager(IApplicationContext context,
                          IEmbeddingManager embeddingManager,
                          ILogger<ChunkManager> logger) : BaseManager(context), IChunkManager
{

    /// <summary>
    /// Adds Chunks
    /// </summary>
    /// <param name="documentid">Injected <see cref="Guid"/></param>
    /// <param name="filecontent">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task AddChunks(Guid @documentid,
                                string @filecontent)
    {
        string @decodedcontent = TextHelper.DecodeToUtf8(@filecontent);

        var @chunks = TextHelper.ChunkText(@decodedcontent);

        var @textchunks = @chunks.Select(c => c.ToString()).ToList();

        var @vectorchunks = await embeddingManager.GetEmbeddings(@textchunks);

        List<Entities.Chunk> @entities =
            [.. textchunks.Zip(vectorchunks, (text, embedding) => (text, embedding))
                      .Select((pair, i) => new Entities.Chunk
                      {
                          DocumentId = documentid,
                          Ordinal = i,
                          Text = pair.text,
                          Embedding = pair.embedding
                      })];


        if (@entities.Count > 0)
        {
            await Context.Chunk.AddRangeAsync(@entities);
            await Context.SaveChangesAsync();

            // Log
            string @logData = $"{nameof(Entities.Document)} {@documentid} added its {@entities.Count} {nameof(Entities.Chunk)}s at {DateTime.UtcNow:t}";

            @logger.LogInformation(@logData);
        }
    }

    /// <summary>
    /// Finds Chunks By Text
    /// </summary>
    /// <param name="text">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{ICollection{Entities.Chunk}}"/></returns>
    public async Task<ICollection<Chunk>> FindByText(string @text)
    {
        var @embedding = await embeddingManager.GetEmbedding(@text);

        return await context.Chunk
            .AsQueryable()
            .VectorSearch(x => x.Embedding, new QueryVector(@embedding), 5)
            .ToListAsync();
    }
}
