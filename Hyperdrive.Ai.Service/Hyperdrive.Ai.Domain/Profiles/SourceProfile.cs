using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Domain.Profiles;

public static class SourceProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Source"/></param>
    /// <returns>Instance of <see cref="SourceDto"/></returns>
    public static SourceDto ToDto(this Source @entity)
    {
        return new SourceDto
        {
            DocumentId = @entity.DocumentId,
            Preview = @entity.Preview,
            LastModified = @entity.ModifiedAt ?? @entity.CreatedAt,
        };
    }

}
