using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Domain.Profiles;

/// <summary>
/// Represents a <see cref="ArrangeProfile"/> class.
/// </summary>
public static class ArrangeProfile
{
    /// <summary>
    /// Transforms to Message Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Arrange"/></param>
    /// <returns>Instance of <see cref="ChatMessageDto"/></returns>
    public static ChatMessageDto ToMessageDto(this Arrange @entity)
    {
        return new ChatMessageDto
        {
            Message = @entity.Content,
            Role = "System"
        };
    }
}
