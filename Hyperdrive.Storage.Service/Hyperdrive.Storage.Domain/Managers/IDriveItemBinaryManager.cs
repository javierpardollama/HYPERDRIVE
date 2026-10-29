using Hyperdrive.Storage.Domain.Dtos;

namespace Hyperdrive.Storage.Domain.Managers;

/// <summary>
/// Represents a <see cref="IDriveItemBinaryManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IDriveItemBinaryManager : IBaseManager
{
    /// <summary>
    /// Finds Latest Drive Item Binary By Drive Item Id
    /// </summary>
    /// <param name="driveitemid">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
    public Task<DriveItemBinaryDto> FindLatestDriveItemBinaryById(Guid @driveitemid);

    /// <summary>
    /// Finds Drive Item Binary By Drive Item Info Id
    /// </summary>
    /// <param name="driveiteminfoid">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
    public Task<DriveItemBinaryDto> FindDriveItemBinaryById(Guid @driveiteminfoid);
}
