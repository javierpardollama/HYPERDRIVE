using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;

namespace Hyperdrive.Intelligence.Domain.Profiles;

/// <summary>
/// Represents a <see cref="InteractionProfile"/> class.
/// </summary>
public static class InteractionProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Interaction"/></param>
    /// <returns>Instance of <see cref="InteractionDto"/></returns>
    public static InteractionDto ToDto(this Interaction @entity)
    {
        return new InteractionDto
        {
            LastModified = @entity.ModifiedAt ?? @entity.CreatedAt,
            Answer = entity.Answer?.ToDto(),
            Query = @entity.Query?.ToDto()
        };
    }
}
