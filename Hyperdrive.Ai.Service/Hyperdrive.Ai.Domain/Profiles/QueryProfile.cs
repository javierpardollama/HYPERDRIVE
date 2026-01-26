using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Domain.Profiles;

/// <summary>
/// Represents a <see cref="QueryProfile"/> class.
/// </summary>
public static class QueryProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Query"/></param>
    /// <returns>Instance of <see cref="QueryDto"/></returns>
    public static QueryDto ToDto(this Query @entity)
    {
        return new QueryDto
        {
            Text = @entity.Text,
            LastModified = @entity.ModifiedAt ?? @entity.CreatedAt,
        };
    }
}
