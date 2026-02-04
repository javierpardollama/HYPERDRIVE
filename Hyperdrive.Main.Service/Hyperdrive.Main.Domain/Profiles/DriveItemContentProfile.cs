using Hyperdrive.Main.Domain.Dtos;
using Hyperdrive.Main.Domain.Entities;
using System;

namespace Hyperdrive.Main.Domain.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemContentProfile"/> class.
/// </summary>
public static class DriveItemContentProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemContent"/></param>
    /// <returns>Instance of <see cref="DriveItemVersionDto"/></returns>
    public static DriveItemVersionDto ToDto(this DriveItemContent @entity)
    {
        return new DriveItemVersionDto
        {
            Id = @entity.Id,
            FileName = @entity.DriveItemInfo.FileName,
            LastModified = @entity.CreatedAt,
            Size = @entity.Size,
            Type = @entity.Type,
            Downloadeable = @entity?.Size.HasValue ?? false
        };
    }

    /// <summary>
    /// Transforms to Binary Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemContent"/></param>
    /// <returns>Instance of <see cref="DriveItemBinaryDto"/></returns>
    public static DriveItemBinaryDto ToBinary(this DriveItemContent @entity)
    {
        return new DriveItemBinaryDto
        {
            FileName = @entity.DriveItemInfo.FileName,
            Data = Convert.ToBase64String(@entity.Data),
            Type = @entity.Type
        };
    }
}