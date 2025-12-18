using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using System;
using System.Linq;

namespace Hyperdrive.Domain.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemProfile"/> class.
/// </summary>
public static class DriveItemProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemInfo"/></param>
    /// <returns>Instance of <see cref="DriveItemDto"/></returns>
    public static DriveItemDto ToDto(this DriveItemInfo @entity)
    {
        return new DriveItemDto
        {
            Id = @entity.DriveItem.Id,
            FileName = @entity.FileName,
            Name = @entity.Name,
            Extension = @entity.Extension,
            By = @entity.DriveItem.By?.ToCatalog(),
            Parent = @entity.DriveItem?.Parent?.ToCatalog(),
            Folder = @entity.DriveItem.Folder,
            LastModified = @entity.CreatedAt,
            SharedWith = [.. @entity.DriveItem.SharedWith.Select(x=> x.User.ToCatalog())],
            Downloadeable = @entity.Content?.Size.HasValue ?? false
        };
    }

    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItem"/></param>
    /// <returns>Instance of <see cref="CatalogDto"/></returns>
    public static CatalogDto ToCatalog(this DriveItem @entity)
    {
        return new CatalogDto
        {
            Id = @entity.Id,
            Name = @entity.Activity
              .OrderByDescending(x => x.CreatedAt)
              .FirstOrDefault()?.FileName
        };
    }
}