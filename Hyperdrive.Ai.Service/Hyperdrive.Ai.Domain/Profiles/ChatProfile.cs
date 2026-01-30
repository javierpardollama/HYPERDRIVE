using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ChatProfile"/> class.
/// </summary>
public static class ChatProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Chat"/></param>
    /// <returns>Instance of <see cref="ChatDto"/></returns>
    public static ChatDto ToDto(this Chat @entity)
    {
        return new ChatDto
        {
            Id = @entity.Id,
            Title = @entity.Title
        };
    }
}