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

/// <summary>
///     Represents a <see cref="ChatManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IChatManager"/>
/// </summary>
public class ChatManager(IApplicationContext context,
                             ILogger<ChatManager> logger) : BaseManager(context), IChatManager
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

    /// <summary>
    /// Removes Chat
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Chat"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task RemoveChat(Entities.Chat @entity)
    {
        try
        {
            Entities.Chat @chat = await FindChatById(@entity.Id);

            @chat.DeletedBy = @entity.DeletedBy;
            @chat.DeletedAt = @entity.DeletedAt;
            @chat.Deleted = @entity.Deleted;

            Context.Chat.Update(@chat);

            await Context.SaveChangesAsync();

            string @logData = $"{nameof(Entities.Chat)} with Id {@chat.Id} was removed at {DateTime.UtcNow:t}";

            logger.LogInformation(@logData);
        }
        catch (DbUpdateConcurrencyException)
        {
            await FindChatById(@entity.Id);
        }
    }

    /// <summary>
    /// Finds Chat By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Chat}"/></returns>
    public async Task<Entities.Chat> FindChatById(Guid @id)
    {
        Entities.Chat @chat = await Context.Chat
        .TagWith("FindChatById")
        .Where(x => x.Id == @id)
        .FirstOrDefaultAsync();

        if (@chat == null)
        {
            string @logData = $"{nameof(Entities.Chat)} with Id {@id} was not found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Entities.Chat)
                                       + " with Id "
                                       + @id
                                       + " does not exist");
        }

        return @chat;
    }

    /// <summary>
    /// Adds Chat
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Chat"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Chat}"/></returns>
    public async Task<Entities.Chat> AddChat(Entities.Chat @entity)
    {
        Entities.Chat @chat = new()
        {
            CreatedBy = entity.CreatedBy
        };

        await Context.Chat.AddAsync(@chat);

        await Context.SaveChangesAsync();

        // Log
        string @logData = $"{nameof(Entities.Chat)} {@chat.Id} was added at {DateTime.UtcNow:t}";

        logger.LogInformation(logData);

        return @chat;
    }
}
