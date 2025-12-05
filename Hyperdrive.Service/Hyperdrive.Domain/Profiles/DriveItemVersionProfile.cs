using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemVersionProfile"/> class.
/// </summary>
public static class DriveItemVersionProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
    /// <returns>Instance of <see cref="DriveItemVersionDto"/></returns>
    public static DriveItemVersionDto ToDto(this DriveItemVersion @entity)
    {
        return new DriveItemVersionDto
        {
            Id = @entity.Id,
            FileName = @entity.FileName,
            LastModified = @entity.CreatedAt,
            Size = @entity.Size,
            Type = @entity.Type,
        };
    }
}