using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;

namespace Hyperdrive.Intelligence.Domain.Profiles;

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