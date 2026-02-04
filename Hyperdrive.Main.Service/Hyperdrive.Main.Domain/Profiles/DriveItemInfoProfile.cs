using Hyperdrive.Main.Domain.Dtos;
using Hyperdrive.Main.Domain.Entities;

namespace Hyperdrive.Main.Domain.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemInfoProfile"/> class.
/// </summary>
public static class DriveItemInfoProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemInfo"/></param>
    /// <returns>Instance of <see cref="DriveItemVersionDto"/></returns>
    public static DriveItemVersionDto ToDto(this DriveItemInfo @entity)
    {
        return new DriveItemVersionDto
        {
            Id = @entity.Id,
            FileName = @entity.FileName,
            LastModified = @entity.CreatedAt,
            Size = @entity.Content.Size,
            Type = @entity.Content.Type,
            Downloadeable = @entity?.Content?.Size.HasValue ?? false
        };
    }
}