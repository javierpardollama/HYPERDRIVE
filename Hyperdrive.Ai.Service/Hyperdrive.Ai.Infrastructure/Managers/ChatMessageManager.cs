using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Domain.Profiles;
using Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="ChatMessageManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IChatMessageManager"/>
/// </summary>
public class ChatMessageManager(IApplicationContext context,
                     ILogger<ChatMessageManager> logger) : BaseManager(context), IChatMessageManager
{
    /// <summary>
    /// Finds Latest Chat Messages By Chat Id
    /// </summary>
    /// <param name="chatid">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{ICollection{ChatMessageDto}}"/></returns>
    public async Task<ICollection<ChatMessageDto>> FindLatestChatMessagesByChatId(Guid @chatid)
    {
        var messages = await Context.Interactions
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.ChatId == chatid)
                .OrderByDescending(x => x.CreatedAt)
                .Take(5)
                .SelectMany(x => new[] { x.Query.ToMessageDto(), x.Answer.ToMessageDto() })
                .ToListAsync();

        return messages;
    }
}
