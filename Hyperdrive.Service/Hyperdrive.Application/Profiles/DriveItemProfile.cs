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
            By = @dto.By?.ToCatalogViewModel(),
            Parent = @dto.Parent?.ToCatalogViewModel(),
            Folder = @dto.Folder,
            LastModified = @dto.LastModified,
            SharedWith = [.. @dto.SharedWith.Select(x=> x.ToCatalogViewModel())],
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
    
    /// <summary>
    /// Transforms to Binary ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="DriveItemBinaryDto"/></param>
    /// <returns>Instance of <see cref="ViewDriveItemBinary"/></returns>
    public static ViewDriveItemBinary ToViewModel(this DriveItemBinaryDto @dto)
    {
        return new ViewDriveItemBinary
        {
            FileName = @dto.FileName,
            Data = @dto.Data,
            Type = @dto.Type
        };
    }
}