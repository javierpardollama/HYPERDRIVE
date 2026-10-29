using Hyperdrive.Storage.Domain.Dtos;
using Hyperdrive.Storage.Domain.Entities;

namespace Hyperdrive.Storage.Domain.Managers;

/// <summary>
/// Represents a <see cref="IDriveItemVersionManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IDriveItemVersionManager : IBaseManager
{
    /// <summary>
    /// Finds Paginated Drive Item Version By Drive Item Id
    /// </summary>
    /// <param name="index">Injected <see cref="int"/></param>
    /// <param name="size">Injected <see cref="int"/></param>
    /// <param name="driveitemid">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{PageDto{DriveItemVersionDto}}"/></returns>
    public Task<PageDto<DriveItemVersionDto>> FindPaginatedDriveItemVersionByDriveItemId(int @index, int @size, Guid @driveitemid);

    /// <summary>
    /// Finds Drive Item Version By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemInfo}"/></returns>
    public Task<DriveItemInfo> FindDriveItemVersionById(Guid @id);

    /// <summary>
    /// Targets Drive Item Version
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemInfo"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public Task TargetDriveItemVersion(DriveItemInfo @entity);
}
