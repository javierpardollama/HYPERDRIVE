using Hyperdrive.Ai.Domain.Dtos;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IChatCompletitionManager"/> interface.
/// </summary>
public interface IChatCompletitionManager
{
    /// <summary>
    /// Gets Chat Completition
    /// </summary>
    /// <param name="messages">Injected <see cref="ICollection{ChatMessageDto}"/></param>
    /// <returns>Instance of <see cref="Task{string}"/></returns>
    public Task<string> GetCompletionAsync(ICollection<ChatMessageDto> messages);
}
