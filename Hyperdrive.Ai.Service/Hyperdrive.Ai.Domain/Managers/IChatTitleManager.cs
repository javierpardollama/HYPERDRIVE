using Hyperdrive.Ai.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IChatTitleManager"/> interface.
/// </summary>
public interface IChatTitleManager
{
    /// <summary>
    /// Gets Chat Title Async
    /// </summary>
    /// <param name="messages">Injected <see cref="ICollection{ChatMessageDto}"/></param>
    /// <returns>Instance of <see cref="Task{string}"/></returns>
    public Task<string> GetChatTitleAsync(ICollection<ChatMessageDto> messages);
}
