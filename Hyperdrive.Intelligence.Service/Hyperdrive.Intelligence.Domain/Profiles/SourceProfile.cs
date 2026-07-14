using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;

namespace Hyperdrive.Intelligence.Domain.Profiles;

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
