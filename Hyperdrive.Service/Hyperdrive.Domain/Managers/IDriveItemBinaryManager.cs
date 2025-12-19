using Hyperdrive.Domain.Dtos;
using System.Threading.Tasks;

namespace Hyperdrive.Domain.Managers;

/// <summary>
/// Represents a <see cref="IDriveItemBinaryManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IDriveItemBinaryManager : IBaseManager
{
    /// <summary>
    /// Finds Latest Drive Item Binary By Drive Item Id
    /// </summary>
    /// <param name="driveitemid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
    public Task<DriveItemBinaryDto> FindLatestDriveItemBinaryById(int @driveitemid);

    /// <summary>
    /// Finds Drive Item Binary By Drive Item Info Id
    /// </summary>
    /// <param name="driveiteminfoid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
    public Task<DriveItemBinaryDto> FindDriveItemBinaryById(int @driveiteminfoid);
}
