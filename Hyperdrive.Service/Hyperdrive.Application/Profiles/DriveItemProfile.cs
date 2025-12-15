using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;
using System.Linq;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationRoleProfile"/> class.
/// </summary>
public static class DriveItemProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="DriveItemDto"/></param>
    /// <returns>Instance of <see cref="ViewDriveItem"/></returns>
    public static ViewDriveItem ToViewModel(this DriveItemDto @dto)
    {
        return new ViewDriveItem
        {
            Id = @dto.Id,
            FileName = @dto.Name,
            Name = @dto.Name,
            Extension = @dto.Extension,
            By = @dto.By?.ToViewModel(),
            Parent = @dto.Parent?.ToViewModel(),
            Folder = @dto.Folder,
            LastModified = @dto.LastModified,
            SharedWith = [.. @dto.SharedWith.Select(x=> x.ToViewModel())],
            Downloadeable = @dto.Downloadeable,
        };
    }
    
    /// <summary>
    /// Transforms to Page ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="PageDto{DriveItemDto}"/></param>
    /// <returns>Instance of <see cref="ViewPage{ViewDriveItem}"/></returns>
    public static ViewPage<ViewDriveItem> ToPageViewModel(this PageDto<DriveItemDto> @dto)
    {
        return new ViewPage<ViewDriveItem>
        {
            Index = @dto.Index,
            Length = @dto.Length,
            Size = @dto.Size,
            Items = [.. dto.Items.Select(x=> x.ToViewModel())]
        };
    }   
}