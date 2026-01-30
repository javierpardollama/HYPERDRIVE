using Hyperdrive.Ai.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IInteractionManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IInteractionManager : IBaseManager
{
    /// <summary>
    /// Adds Interaction
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Interaction"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Interaction}"/></returns>
    public Task<Entities.Interaction> AddInteraction(Entities.Interaction @entity);

    /// <summary>
    /// Reloads Interaction By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{InteractionDto}"/></returns>
    public Task<InteractionDto> ReloadInteractionById(Guid @id);

    /// <summary>
    ///     Finds Paginated Interaction
    /// </summary>
    /// <param name="index">Injected <see cref="int" /></param>
    /// <param name="size">Injected <see cref="int" /></param>
    /// <param name="chatid">Injected <see cref="Guid" /></param>
    /// <returns>Instance of <see cref="Task{PageDto{InteractionDto}}" /></returns>
    public Task<PageDto<InteractionDto>> FindPaginatedInteraction(int @index, int @size, Guid chatid);
}
