using Hyperdrive.Ai.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IChatManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IChatManager : IBaseManager
{
    /// <summary>
    /// Reloads Chat By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{ChatDto}"/></returns>
    public Task<ChatDto> ReloadChatById(Guid @id);

    /// <summary>
    /// Removes Chat
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Chat"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public Task RemoveChat(Entities.Chat @entity);

    /// <summary>
    /// Finds Chat By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Chat}"/></returns>
    public Task<Entities.Chat> FindChatById(Guid @id);
}
