using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Managers;
using OpenAI.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="ChatCompletitionManager" /> class. Implements <see cref="IChatCompletitionManager"/>
/// </summary>
public class ChatCompletitionManager : IChatCompletitionManager
{
    private readonly ChatClient Client;

    private readonly IChatMessageManager ChatMessageManager;

    /// <summary>
    ///     Initializes a new Instance of <see cref="ChatCompletitionManager" />
    /// </summary>
    /// <param name="client">Injected <see cref="ChatClient" /></param>
    public ChatCompletitionManager(ChatClient client, IChatMessageManager chatMessageManager)
    {
        Client = client;
        ChatMessageManager = chatMessageManager;
    }

    /// <summary>
    /// Gets Chat Completition
    /// </summary>
    /// <param name="messages">Injected <see cref="ICollection{ChatMessageDto}"/></param>
    /// <returns>Instance of <see cref="Task{string}"/></returns>
    public async Task<string> GetCompletionAsync(ICollection<ChatMessageDto> messages)
    {
        var msgs = messages.Select(x => x.Role switch
        {
            "User" => new UserChatMessage(x.Message),
            "Assistant" => new AssistantChatMessage(x.Message),
            "System" => (ChatMessage)new SystemChatMessage(x.Message),
            _ => throw new System.NotImplementedException(),
        }).ToList();

        var reply = await Client.CompleteChatAsync(msgs);

        return reply.Value.Content[0].Text;
    }
}
