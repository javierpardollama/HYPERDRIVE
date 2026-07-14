using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;

namespace Hyperdrive.Intelligence.Domain.Profiles;

/// <summary>
/// Represents a <see cref="SetupProfile"/> class.
/// </summary>
public static class SetupProfile
{
    /// <summary>
    /// Transforms to Message Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Setup"/></param>
    /// <returns>Instance of <see cref="ChatMessageDto"/></returns>
    public static ChatMessageDto ToMessageDto(this Setup @entity)
    {
        return new ChatMessageDto
        {
            Message = @entity.Content,
            Role = "System"
        };
    }
}
