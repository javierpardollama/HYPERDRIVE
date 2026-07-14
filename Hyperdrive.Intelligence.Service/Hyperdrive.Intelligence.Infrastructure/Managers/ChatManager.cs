using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Exceptions;
using Hyperdrive.Intelligence.Domain.Managers;
using Hyperdrive.Intelligence.Domain.Profiles;
using Hyperdrive.Intelligence.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Chat = Hyperdrive.Intelligence.Domain.Entities.Chat;

namespace Hyperdrive.Intelligence.Infrastructure.Managers;

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
        .AsNoTracking()
        .AsSplitQuery()
        .TagWith("ReloadChatById")
        .Where(x => x.Id == @id)
        .Select(x => x.ToDto())
        .FirstOrDefaultAsync();

        if (@chat is null)
        {
            string @logData = $"{nameof(Chat)} with Id {@id} was not found at at {DateTime.UtcNow:t}";

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(Chat)
                                       + " does not exist");
        }

        return @chat;
    }

    /// <summary>
    /// Removes Chat
    /// </summary>
    /// <param name="entity">Injected <see cref="Chat"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task RemoveChat(Chat @entity)
    {
        try
        {
            Chat @chat = await FindChatById(@entity.Id);

            @chat.DeletedBy = @entity.DeletedBy;
            @chat.DeletedAt = @entity.DeletedAt;
            @chat.Deleted = @entity.Deleted;

            Context.Chat.Update(@chat);

            await Context.SaveChangesAsync();

            string @logData = $"{nameof(Chat)} with Id {@chat.Id} was removed at {DateTime.UtcNow:t}";

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
    public async Task<Chat> FindChatById(Guid @id)
    {
        Chat @chat = await Context.Chat
        .TagWith("FindChatById")
        .Where(x => x.Id == @id)
        .FirstOrDefaultAsync();

        if (@chat == null)
        {
            string @logData = $"{nameof(Chat)} with Id {@id} was not found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Chat)
                                       + " with Id "
                                       + @id
                                       + " does not exist");
        }

        return @chat;
    }

    /// <summary>
    /// Adds Chat
    /// </summary>
    /// <param name="entity">Injected <see cref="Chat"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Chat}"/></returns>
    public async Task<Chat> AddChat(Chat @entity)
    {
        Chat @chat = new()
        {
            CreatedBy = entity.CreatedBy
        };

        await Context.Chat.AddAsync(@chat);

        await Context.SaveChangesAsync();

        // Log
        string @logData = $"{nameof(Chat)} {@chat.Id} was added at {DateTime.UtcNow:t}";

        logger.LogInformation(logData);

        return @chat;
    }

    /// <summary>
    ///     Finds Paginated Chat
    /// </summary>
    /// <param name="index">Injected <see cref="int" /></param>
    /// <param name="size">Injected <see cref="int" /></param>
    /// <param name="userid">Injected <see cref="Guid" /></param>
    /// <returns>Instance of <see cref="Task{PageDto{ChatDto}}" /></returns>
    public async Task<PageDto<ChatDto>> FindPaginatedChat(int @index, int @size, Guid @userid)
    {
        PageDto<ChatDto> @page = new()
        {
            Length = await Context.Chat
                        .TagWith("CountAllInteraction")
                        .AsNoTracking()
                        .AsSplitQuery()
                        .Where(x => x.CreatedBy == userid)
                        .CountAsync(),
            Index = @index,
            Size = @size,
            Items = await Context.Chat
                        .TagWith("FindPaginatedInteraction")
                        .AsNoTracking()
                        .AsSplitQuery()
                         .Where(x => x.CreatedBy == userid)
                        .OrderByDescending(x => x.CreatedAt)
                        .Skip(@index * @size)
                        .Take(@size)
                        .Select(x => x.ToDto())
                        .ToListAsync()
        };

        return @page;
    }

    /// <summary>
    /// Updates Chat
    /// </summary>
    /// <param name="entity">Injected <see cref="Chat"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Chat}"/></returns>
    public async Task<Chat> UpdateChat(Chat @entity)
    {
        Chat @chat = await FindChatById(@entity.Id);
        @chat.Title = @entity.Title.Trim();
        @chat.ModifiedBy = @entity.ModifiedBy;
        chat.ModifiedAt = @entity.ModifiedAt;

        Context.Chat.Update(@chat);

        await Context.SaveChangesAsync();

        // Log
        var logData = nameof(Chat)
                      + " with Id "
                      + @entity.Id
                      + " was modified at "
                      + DateTime.Now.ToShortTimeString();

        logger.LogInformation(logData);

        return @chat;
    }
}
