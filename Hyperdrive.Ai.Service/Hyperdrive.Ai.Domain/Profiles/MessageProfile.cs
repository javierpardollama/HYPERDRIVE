using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Domain.Profiles;

/// <summary>
/// Represents a <see cref="MessageProfile"/> class.
/// </summary>
public static class MessageProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Message"/></param>
    /// <returns>Instance of <see cref="ChatDto"/></returns>
    public static MessageDto ToDto(this Message @entity)
    {
        return new MessageDto
        {
            Id = @entity.Id,
            CreatedAt = @entity.CreatedAt,
            Text = entity.Text
        };
    }
}