using Hyperdrive.Ai.Domain.Entities;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Domain.Profiles;
using OpenAI.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="ChatCompletitionManager" /> class. Implements <see cref="IChatCompletitionManager"/>
/// </summary>
public class ChatCompletitionManager : IChatCompletitionManager
{
    private readonly ChatClient Client;

    /// <summary>
    ///     Initializes a new Instance of <see cref="ChatCompletitionManager" />
    /// </summary>
    /// <param name="client">Injected <see cref="ChatClient" /></param>
    public ChatCompletitionManager(ChatClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Gets Chat Completition
    /// </summary>
    /// <param name="query">Injected <see cref="Query"/></param>
    /// <param name="chunks">Injected <see cref="ICollection{Entities.Chunk}"/></param>
    /// <returns>Instance of <see cref="Answer"/></returns>
    public async Task<Answer> GetCompletionAsync(Query query, ICollection<Entities.Chunk> chunks)
    {
        var reply = await Client.CompleteChatAsync(
            [
                new SystemChatMessage(query.System),
                new UserChatMessage(query.User)
            ]);

        return new Answer
        {
            Text = reply.Value.Content[0].Text,
            Sources = [.. chunks.Select(c => c?.ToSource())]
        };
    }
}
