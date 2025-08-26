using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="DriveItemVersionProfile"/> class.
/// </summary>
public static class DriveItemVersionProfile
{
    /// <summary>
    /// Transforms to Dto
    /// </summary>
    /// <param name="dto">Injected <see cref="DriveItemVersionDto"/></param>
    /// <returns>Instance of <see cref="ViewDriveItemVersion"/></returns>
    public static ViewDriveItemVersion ToDto(this DriveItemVersionDto @dto)
    {
        return new ViewDriveItemVersion
        {
            Id = @dto.Id,
            LastModified = @dto.LastModified,
            Data = @dto.Data,
            Size = @dto.Size,
            Type = @dto.Type,
        };
    }
}