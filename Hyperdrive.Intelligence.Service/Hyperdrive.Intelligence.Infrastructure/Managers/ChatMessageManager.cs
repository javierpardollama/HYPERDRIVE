using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Managers;
using Hyperdrive.Intelligence.Domain.Profiles;
using Hyperdrive.Intelligence.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hyperdrive.Intelligence.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="ChatMessageManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IChatMessageManager"/>
/// </summary>
public class ChatMessageManager(IApplicationContext context) : BaseManager(context), IChatMessageManager
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
