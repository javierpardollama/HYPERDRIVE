using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Managers;
using OpenAI.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="ChatSummaryManager" /> class. Implements <see cref="IChatSummaryManager"/>
/// </summary>
public class ChatSummaryManager : IChatSummaryManager
{
    private readonly ChatClient Client;

    /// <summary>
    ///     Initializes a new Instance of <see cref="ChatSummaryManager" />
    /// </summary>
    /// <param name="client">Injected <see cref="ChatClient" /></param>
    public ChatSummaryManager(ChatClient client)
    {
        Client = client;
    }

    /// <summary>
    /// Gets Chat Summary Async
    /// </summary>
    /// <param name="messages">Injected <see cref="ICollection{ChatMessageDto}"/></param>
    /// <returns>Instance of <see cref="Task{string}"/></returns>
    public async Task<string> GetChatSummaryAsync(ICollection<ChatMessageDto> messages)
    {
        var msgs = messages.Select(x => x.Role switch
        {
            "User" => (ChatMessage)new UserChatMessage(x.Message),
            "Assistant" => (ChatMessage)new AssistantChatMessage(x.Message),
            _ => throw new System.NotImplementedException(),
        }).Append(new SystemChatMessage(@"You are a helpful assistant. Summarize the conversation for a busy reader.
                Return:
                3–6 bullet key points 
                Decisions made 
                Open questions 
                next steps.
                Keep it under 120 words, neutral tone."
            ));

        var reply = await Client.CompleteChatAsync(msgs);

        return reply.Value.Content[0].Text;
    }

}
