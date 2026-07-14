using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;

namespace Hyperdrive.Intelligence.Domain.Profiles;

/// <summary>
///     Represents a <see cref="DocumentProfile" /> class
/// </summary>
public static class DocumentProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="Document"/></param>
    /// <returns>Instance of <see cref="ChatDto"/></returns>
    public static DocumentDto ToDto(this Document @entity)
    {
        return new DocumentDto
        {
            Id = @entity.Id,
            LastModified = @entity.ModifiedAt ?? @entity.CreatedAt,
            Name = entity.FileName               
        };
    }
}
