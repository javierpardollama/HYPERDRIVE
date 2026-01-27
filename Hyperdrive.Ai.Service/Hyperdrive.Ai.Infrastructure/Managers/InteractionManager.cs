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
///     Represents a <see cref="InteractionManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IInteractionManager"/>
/// </summary>
public class InteractionManager(IApplicationContext context,
                         ILogger<InteractionManager> logger) : BaseManager(context), IInteractionManager
{
    /// <summary>
    /// Adds Interaction
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Interaction"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Interaction}"/></returns>
    public async Task<Entities.Interaction> AddInteraction(Entities.Interaction entity)
    {
        Entities.Interaction @interaction = new()
        {
            ChatId = entity.ChatId,
            CreatedBy = entity.CreatedBy,
            Answer = entity.Answer,
            Query = entity.Query,
        };

        await Context.Interactions.AddAsync(@interaction);

        await Context.SaveChangesAsync();

        // Log
        string @logData = $"{nameof(Entities.Interaction)} {@interaction.Id} was added at {DateTime.UtcNow:t}";

        logger.LogInformation(logData);

        return @interaction;
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
            string @logData = $"{nameof(Entities.Interaction)} with Id {@id} was not found at at {DateTime.UtcNow:t}";

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(Entities.Interaction)
                                       + " does not exist");
        }

        return @interaction;
    }
}
