using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;
using Hyperdrive.Intelligence.Domain.Exceptions;
using Hyperdrive.Intelligence.Domain.Managers;
using Hyperdrive.Intelligence.Domain.Profiles;
using Hyperdrive.Intelligence.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Intelligence.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="InteractionManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IInteractionManager"/>
/// </summary>
public class InteractionManager(IApplicationContext context,
                         ILogger<InteractionManager> logger) : BaseManager(context), IInteractionManager
{
    /// <summary>
    /// Adds Interaction
    /// </summary>
    /// <param name="entity">Injected <see cref="Interaction"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Interaction}"/></returns>
    public async Task<Interaction> AddInteraction(Interaction entity)
    {
        Interaction @interaction = new()
        {
            ChatId = entity.ChatId,
            CreatedBy = entity.CreatedBy,
            Setup = entity.Setup,
            Summary = entity.Summary,
            Answer = entity.Answer,
            Query = entity.Query,
        };

        await Context.Interactions.AddAsync(@interaction);

        await Context.SaveChangesAsync();

        // Log
        string @logData = $"{nameof(Interaction)} {@interaction.Id} was added at {DateTime.UtcNow:t}";

        logger.LogInformation(logData);

        return @interaction;
    }

    /// <summary>
    ///     Finds Paginated Interaction
    /// </summary>
    /// <param name="index">Injected <see cref="int" /></param>
    /// <param name="size">Injected <see cref="int" /></param>
    /// <param name="chatid">Injected <see cref="Guid" /></param>
    /// <returns>Instance of <see cref="Task{PageDto{InteractionDto}}" /></returns>
    public async Task<PageDto<InteractionDto>> FindPaginatedInteraction(int index, int size, Guid chatid)
    {
        PageDto<InteractionDto> @page = new()
        {
            Length = await Context.Interactions
                        .TagWith("CountAllInteraction")
                        .AsNoTracking()
                        .AsSplitQuery()
                        .Where(x => x.ChatId == @chatid)
                        .CountAsync(),
            Index = @index,
            Size = @size,
            Items = await Context.Interactions
                        .TagWith("FindPaginatedInteraction")
                        .AsNoTracking()
                        .AsSplitQuery()
                        .Where(x => x.ChatId == @chatid)
                        .OrderByDescending(x => x.CreatedAt)
                        .Skip(@index * @size)
                        .Take(@size)
                        .Select(x => x.ToDto())
                        .ToListAsync()
        };

        return @page;
    }

    /// <summary>
    /// Reloads Interaction By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{InteractionDto}"/></returns>
    public async Task<InteractionDto> ReloadInteractionById(Guid @id)
    {
        InteractionDto @interaction = await Context.Interactions
              .TagWith("ReloadInteractionById")
              .Where(x => x.Id == @id)
              .Select(x => x.ToDto())
              .FirstOrDefaultAsync();

        if (@interaction is null)
        {
            string @logData = $"{nameof(Interaction)} with Id {@id} was not found at at {DateTime.UtcNow:t}";

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(Interaction)
                                       + " does not exist");
        }

        return @interaction;
    }
}
