using Hyperdrive.Ai.Domain.Dtos;
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
    /// <param name="text">Injected <see cref="string"/></param>
    /// <param name="chunks">Injected <see cref="List{Entities.Chunk}"/></param>
    /// <returns>Instance of <see cref="RagAnswerDto"/></returns>
    public async Task<RagAnswerDto> GetCompletionAsync(string text, List<Entities.Chunk> chunks)
    {

        string contextText = string.Join("\n\n---\n\n",
             chunks.Select((c, i) => $"[Chunk {i + 1}] {c.Text}"));

        string system =
        @"You are a helpful assistant. Answer strictly using the provided context.
        If the answer is not in the context, say “I don’t know.” Cite chunk numbers.";

        string user =
        $@"Question: {text}
        Context:
        `{contextText}
        Instructions:
        - Answer concisely (<= 6 sentences).
        - Include citations like [Chunk 2], [Chunk 4] where applicable.";

        var reply = await Client.CompleteChatAsync(
            [
                new SystemChatMessage(system),
                new UserChatMessage(user)
            ]);

        return new RagAnswerDto
        {
            Answer = reply.Value.Content[0].Text,
            Sources = [.. chunks.Select(c => c?.ToRagDto())]
        };
    }
}
