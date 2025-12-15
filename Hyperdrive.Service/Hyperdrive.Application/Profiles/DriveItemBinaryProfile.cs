using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemBinaryProfile"/> class.
/// </summary>
public static class DriveItemBinaryProfile
{
    /// <summary>
    /// Transforms to ViewModel
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
