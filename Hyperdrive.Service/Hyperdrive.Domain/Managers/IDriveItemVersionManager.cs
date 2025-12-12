using Hyperdrive.Domain.Dtos;
using System.Threading.Tasks;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IDriveItemManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IDriveItemVersionManager : IBaseManager
    {
        /// <summary>
        /// Finds Paginated Drive Item Version By Drive Item Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="driveitemid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemVersionDto}}"/></returns>
        Task<PageDto<DriveItemVersionDto>> FindPaginatedDriveItemVersionByDriveItemId(int @index, int @size, int @driveitemid);

        /// <summary>
        /// Finds Drive Item Binary By Id
        /// </summary>
        /// <param name="driveitemversionid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
        public Task<DriveItemBinaryDto> FindDriveItemBinaryById(int @driveitemversionid);
    }
}
