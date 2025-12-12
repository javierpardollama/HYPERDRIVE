using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;
using System.Linq;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemVersionProfile"/> class.
/// </summary>
public static class DriveItemVersionProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="DriveItemVersionDto"/></param>
    /// <returns>Instance of <see cref="ViewDriveItemVersion"/></returns>
    public static ViewDriveItemVersion ToViewModel(this DriveItemVersionDto @dto)
    {
        return new ViewDriveItemVersion
        {
            Id = @dto.Id,
            FileName = @dto.FileName,
            LastModified = @dto.LastModified,
            Size = @dto.Size,
            Type = @dto.Type,
            Downloadeable = @dto.Downloadeable,
        };
    }

    /// <summary>
    /// Transforms to Page ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="PageDto{DriveItemVersionDto}"/></param>
    /// <returns>Instance of <see cref="ViewPage{ViewDriveItemVersion}"/></returns>
    public static ViewPage<ViewDriveItemVersion> ToPageViewModel(this PageDto<DriveItemVersionDto> @dto)
    {
        return new ViewPage<ViewDriveItemVersion>
        {
            Index = @dto.Index,
            Length = @dto.Length,
            Size = @dto.Size,
            Items = [.. dto.Items.Select(x => x.ToViewModel())]
        };
    }
}