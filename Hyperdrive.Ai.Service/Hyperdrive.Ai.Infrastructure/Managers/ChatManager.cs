using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Exceptions;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Domain.Profiles;
using Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Infrastructure.Managers;

public class ChatManager(IApplicationContext context,
                             ILogger<DocumentManager> logger) : BaseManager(context), IChatManager
{
    /// <summary>
    /// Reloads Chat By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{ChatDto}"/></returns>
    public async Task<ChatDto> ReloadChatById(Guid @id)
    {
        ChatDto @chat = await Context.Chat
        .TagWith("ReloadChatById")
        .Where(x => x.Id == @id)
        .Select(x => x.ToDto())
        .FirstOrDefaultAsync();

        if (@chat is null)
        {
            string @logData = $"{nameof(Entities.Chat)} with Id {@id} was not found at at {DateTime.UtcNow:t}";

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(Entities.Chat)
                                       + " does not exist");
        }

        return @chat;
    }
}
