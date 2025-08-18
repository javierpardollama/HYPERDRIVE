using System.Collections.Generic;
using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IDriveItemManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IDriveItemManager : IBaseManager
    {
        /// <summary>
        /// Finds Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> FindDriveItemById(int @id);

        /// <summary>
        /// Removes Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task RemoveDriveItemById(int @id);

        /// <summary>
        /// Finds All Drive Item
        /// </summary>
        /// <returns>Instance of <see cref="Task{IList{DriveItemDto}}"/></returns>
        Task<IList<DriveItemDto>> FindAllDriveItem();

        /// <summary>
        /// Finds Paginated Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @id);

        /// <summary>
        /// Finds Paginated Shared Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemByApplicationUserId(int @index, int @size, int @id);

        /// <summary>
        /// Finds All Drive Item Version By Drive Item Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{IList{DriveItemVersionDto}}"/></returns>
        Task<IList<DriveItemVersionDto>> FindAllDriveItemVersionByDriveItemId(int @id);
      
        /// <summary>
        /// Adds Drive Item
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemDto}"/></returns>
        Task<DriveItemDto> AddDriveItem(DriveItem @entity);

        /// <summary>
        /// Adds Application User Drive Item
        /// </summary>
        /// <param name="users">Injected <see cref="int"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        void AddApplicationUserDriveItem(List<int> @users, DriveItem @entity);

        /// <summary>
        /// Adds Drive Item Version
        /// </summary>
        /// <param name="version">Injected <see cref="DriveItemVersion"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        void AddDriveItemVersion(DriveItemVersion @version, DriveItem @entity);

        /// <summary>
        /// Updates Drive Item
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemDto}"/></returns>
        Task<DriveItemDto> UpdateDriveItem(DriveItem @entity);

        /// <summary>
        /// Updates Application User Drive Item
        /// </summary>
        /// <param name="users">Injected <see cref="int"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        void UpdateApplicationUserDriveItem(List<int> @users, DriveItem @entity);

        /// <summary>
        /// Updates Drive Item Version
        /// </summary>
        /// <param name="version">Injected <see cref="DriveItemVersion"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        void UpdateDriveItemVersion(DriveItemVersion @version, DriveItem @entity);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> CheckName(string @name);

        
        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> CheckName(string @name, int @id);
    }
}
