using Hyperdrive.Ai.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IChatMessageManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IChatMessageManager : IBaseManager
{
    /// <summary>
    /// Finds Latest Chat Messages By Chat Id
    /// </summary>
    /// <param name="chatid">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{ICollection{ChatMessageDto}}"/></returns>
    public Task<ICollection<ChatMessageDto>> FindLatestChatMessagesByChatId(Guid @chatid);
}
