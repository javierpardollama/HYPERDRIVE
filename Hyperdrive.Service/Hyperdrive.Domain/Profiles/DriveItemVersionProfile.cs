using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using System;

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

    /// <summary>
    /// Transforms to Binary Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
    /// <returns>Instance of <see cref="DriveItemBinaryDto"/></returns>
    public static DriveItemBinaryDto ToBinary(this DriveItemVersion @entity)
    {
        return new DriveItemBinaryDto
        {
            FileName = @entity.FileName,
            Data = Convert.ToBase64String(@entity.Data),
            Type = @entity.Type
        };
    }
}