using Hyperdrive.Ai.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IChatSummaryManager"/> interface.
/// </summary>
public interface IChatSummaryManager
{
    /// <summary>
    /// Gets Chat Summary Async
    /// </summary>
    /// <param name="messages">Injected <see cref="ICollection{ChatMessageDto}"/></param>
    /// <returns>Instance of <see cref="Task{string}"/></returns>
    public Task<string> GetChatSummaryAsync(ICollection<ChatMessageDto> messages);
}
