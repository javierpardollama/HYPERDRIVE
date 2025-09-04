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
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @userid, int? parent);

        /// <summary>
        /// Finds Paginated Shared Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemWithApplicationUserId(int @index, int @size, int @userid);

        /// <summary>
        /// Finds All Drive Item Version By Drive Item Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{IList{DriveItemVersionDto}}"/></returns>
        Task<IList<DriveItemVersionDto>> FindAllDriveItemVersionByDriveItemId(int @id);

        /// <summary>
        /// Adds Drive Item
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <param name="folder">Injected <see cref="bool"/></param>
        /// <param name="by">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> AddDriveItem(string @filename, int? parent, bool folder, ApplicationUser @by);

        /// <summary>
        /// Adds Shared With
        /// </summary>
        /// <param name="users">Injected <see cref="List{ApplicationUser}"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        Task AddSharedWith(IList<ApplicationUser> @users, DriveItem @entity);

        /// <summary>
        /// Adds Activity
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        /// <param name="type">Injected <see cref="string"/></param>
        /// <param name="size">Injected <see cref="float?"/></param>
        /// <param name="data">Injected <see cref="string"/></param>
        Task AddActivity(DriveItem @entity, string @type, float? @size, string @data);

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> ChangeName(string @name, string @extension, int @id, int? @parent);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> CheckName(string @name, int? @parent);
        
        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> CheckName(string @name, string @extension, int @id, int? @parent);
        
        /// <summary>
        /// Finds Drive Item Binary By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
        Task<DriveItemBinaryDto> FindDriveItemBinaryById(int @id);
        
        /// <summary>
        /// Reloads Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemDto}"/></returns>
        Task<DriveItemDto> ReloadDriveItemById(int @id);
    }
}
