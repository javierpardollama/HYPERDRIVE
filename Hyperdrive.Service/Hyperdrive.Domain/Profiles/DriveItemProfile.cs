using System.Linq;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

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
            FileName = @entity.FileName,
            Name = @entity.Name,
            Extension = @entity.Extension,
            By = @entity.By.ToCatalog(),
            Parent = @entity.Parent.ToCatalog(),
            Folder = @entity.Folder,
            LastModified = @entity.Activity
              .OrderByDescending(x=>x.LastModified)
              .LastOrDefault()?.LastModified,
            SharedWith = @entity.SharedWith
                .Select(x=> x.ApplicationUser.ToCatalog())
                .ToList(),
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
            Name = @entity.FileName
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
            FileName = @entity.FileName,
            Data = @entity.Activity.OrderByDescending(x=> x.LastModified).FirstOrDefault()?.Data,
            Type = @entity.Activity.OrderByDescending(x=> x.LastModified).FirstOrDefault()?.Type
        };
    }
}