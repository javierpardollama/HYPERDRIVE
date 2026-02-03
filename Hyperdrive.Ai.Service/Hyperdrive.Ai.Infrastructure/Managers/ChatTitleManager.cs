using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Managers;
using OpenAI.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="ChatTitleManager" /> class. Implements <see cref="IChatTitleManager"/>
/// </summary>
public class ChatTitleManager : IChatTitleManager
{
    private readonly ChatClient Client;

    /// <summary>
    ///     Initializes a new Instance of <see cref="ChatTitleManager" />
    /// </summary>
    /// <param name="client">Injected <see cref="ChatClient" /></param>
    public ChatTitleManager(ChatClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Gets Chat Title Async
    /// </summary>
    /// <param name="messages">Injected <see cref="ICollection{ChatMessageDto}"/></param>
    /// <returns>Instance of <see cref="Task{string}"/></returns>
    public async Task<string> GetChatTitleAsync(ICollection<ChatMessageDto> messages)
    {
        var msgs = messages.Select(x => x.Role switch
        {
            "User" => (ChatMessage)new UserChatMessage(x.Message),
            "Assistant" => (ChatMessage)new AssistantChatMessage(x.Message),
            _ => throw new System.NotImplementedException(),
        }).Append(new SystemChatMessage(@"
                                        Generate a short title (3–6 words) describing this conversation. 
                                        Do not include dates or personal info. 
                                        Return only the title.
                                        "
            ));

        var reply = await Client.CompleteChatAsync(msgs);

        return reply.Value.Content[0].Text;
    }
}
