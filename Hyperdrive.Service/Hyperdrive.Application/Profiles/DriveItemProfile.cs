using System.Linq;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

public static class DriveItemProfile
{
    public static ViewDriveItem ToDto(this DriveItemDto @dto)
    {
        return new ViewDriveItem
        {
            Id = @dto.Id,
            Name = @dto.Name,
            By = @dto.By.ToCatalogViewModel(),
            Parent = @dto.Parent.ToCatalogViewModel(),
            Folder = @dto.Folder,
            Locked = @dto.Locked,
            LastModified = @dto.LastModified,
            SharedWith = @dto.SharedWith
                .Select(x=> x.ToCatalogViewModel())
                .ToList(),
        };
    }
}