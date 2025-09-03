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
        Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemWithApplicationUserId(int @index, int @size, int @id);

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
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> AddDriveItem(DriveItem @entity);

        /// <summary>
        /// Adds Application User Drive Item
        /// </summary>
        /// <param name="userDriveItems">Injected <see cref="List{ApplicationUserDriveItem}"/></param>
        Task AddApplicationUserDriveItem(List<ApplicationUserDriveItem> @userDriveItems);

        /// <summary>
        /// Adds Drive Item Version
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
        Task AddDriveItemVersion(DriveItemVersion @entity);

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> ChangeName(string @name, int @id, int @parent);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> CheckName(string @name, int @parent);
        
        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> CheckName(string @name, int @id, int @parent);
        
        /// <summary>
        /// Creates Root Drive Item
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> CreateRoot(ApplicationUser @user);
        
        /// <summary>
        /// Finds Drive Item Binary By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
        Task<DriveItemBinaryDto> FindDriveItemBinaryById(int @id);
    }
}
