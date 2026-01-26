using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Entities;
using System.Linq;

namespace Hyperdrive.Ai.Domain.Profiles;

/// <summary>
/// Represents a <see cref="AnswerProfile"/> class.
/// </summary>
public static class AnswerProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Answer"/></param>
    /// <returns>Instance of <see cref="AnswerDto"/></returns>
    public static AnswerDto ToDto(this Answer @entity)
    {
        return new AnswerDto
        {
            Text = @entity.Text,
            LastModified = @entity.ModifiedAt ?? @entity.CreatedAt,
            Sources = [.. entity.Sources.Select(s => s?.ToDto())]
        };
    }
}
