using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using System;

namespace Hyperdrive.Domain.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemInfoProfile"/> class.
/// </summary>
public static class DriveItemInfoProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
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
            Downloadeable = @entity.Content?.Size.HasValue ?? false
        };
    }

    /// <summary>
    /// Transforms to Binary Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
    /// <returns>Instance of <see cref="DriveItemBinaryDto"/></returns>
    public static DriveItemBinaryDto ToBinary(this DriveItemInfo @entity)
    {
        return new DriveItemBinaryDto
        {
            FileName = @entity.FileName,
            Data = Convert.ToBase64String(@entity.Content.Data),
            Type = @entity.Content.Type
        };
    }
}