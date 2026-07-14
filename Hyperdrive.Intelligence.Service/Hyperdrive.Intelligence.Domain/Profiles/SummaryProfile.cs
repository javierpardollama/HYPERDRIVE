using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;

namespace Hyperdrive.Intelligence.Domain.Profiles;

/// <summary>
/// Represents a <see cref="SummaryProfile"/> class.
/// </summary>
public static class SummaryProfile
{
    /// <summary>
    /// Transforms to Message Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Summary"/></param>
    /// <returns>Instance of <see cref="ChatMessageDto"/></returns>
    public static ChatMessageDto ToMessageDto(this Summary @entity)
    {
        return new ChatMessageDto
        {
            Message = @entity.Content,
            Role = "Assistant"
        };
    }
}
