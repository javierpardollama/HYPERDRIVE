using System.Linq;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

public static class DriveItemProfile
{
    public static DriveItemDto ToDto(this DriveItem @entity)
    {
        return new DriveItemDto
        {
            Id = @entity.Id,
            Name = @entity.Name,
            By = @entity.By.ToCatalog(),
            Parent = @entity.Parent.ToCatalog(),
            Folder = @entity.Folder,
            Locked = @entity.Locked,
            LastModified = @entity.DriveItemVersions
              .OrderByDescending(x=>x.LastModified)
              .LastOrDefault()?.LastModified,
            SharedWith = @entity.SharedWith
                .Select(x=> x.ApplicationUser.ToCatalog())
                .ToList(),
        };
    }

    public static CatalogDto ToCatalog(this DriveItem @entity)
    {
        return new CatalogDto
        {
            Id = @entity.Id,
            Name = @entity.Name
        };
    }
}