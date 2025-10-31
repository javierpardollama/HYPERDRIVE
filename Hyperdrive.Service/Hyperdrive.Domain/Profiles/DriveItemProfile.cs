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
    /// <param name="entity">Injected <see cref="DriveItem"/></param>
    /// <returns>Instance of <see cref="DriveItemDto"/></returns>
    public static DriveItemDto ToDto(this DriveItem @entity)
    {
        return new DriveItemDto
        {
            Id = @entity.Id,
            FileName = @entity.Activity
              .OrderByDescending(x => x.CreatedAt)
              .LastOrDefault()?.FileName,
            Name = @entity.Activity
              .OrderByDescending(x => x.CreatedAt)
              .LastOrDefault()?.Name,
            Extension = @entity.Activity
              .OrderByDescending(x => x.CreatedAt)
              .LastOrDefault()?.Extension,
            By = @entity.By?.ToCatalog(),
            Parent = @entity.Parent?.ToCatalog(),
            Folder = @entity.Folder,
            LastModified = @entity.Activity
              .OrderByDescending(x=>x.CreatedAt)
              .LastOrDefault()?.CreatedAt,
            SharedWith = [.. @entity.SharedWith.Select(x=> x.User.ToCatalog())],
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
              .LastOrDefault()?.FileName
        };
    }
    
    /// <summary>
    /// Transforms to Binary Dto
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
    /// <returns>Instance of <see cref="DriveItemBinaryDto"/></returns>
    public static DriveItemBinaryDto ToBinary(this DriveItem @entity)
    {
        return new DriveItemBinaryDto
        {
            FileName = @entity.Activity
              .OrderByDescending(x => x.CreatedAt)
              .LastOrDefault()?.FileName,
            Data = Convert.ToBase64String(@entity.Activity.Where(x => x.Data is not null).OrderByDescending(x=> x.CreatedAt).First().Data),
            Type = @entity.Activity.Where(x=> x.Type is not null).OrderByDescending(x=> x.CreatedAt).First().Type
        };
    }
}