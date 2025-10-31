using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<DriveItem> FindDriveItemById(int? @id);

        /// <summary>
        /// Finds Drive Item By FileName
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>       
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> FindDriveItemByFileName(string @filename, int? @parent, int @userid);

        /// <summary>
        /// Removes Drive Item
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task RemoveDriveItem(DriveItem @entity);

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
        /// <param name="@byi">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        Task<DriveItem> AddDriveItem(string @filename, int? parent, bool @folder, int @byi);

        /// <summary>
        /// Adds Shared With
        /// </summary>
        /// <param name="users">Injected <see cref="List{ApplicationUser}"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        Task AddSharedWith(IList<ApplicationUser> @users, DriveItem @entity);

        /// <summary>
        /// Adds Activity
        /// </summary>
        /// <param name="driveitemid">Injected <see cref="int"/></param>
        /// <param name="filename">Injected <see cref="string"/></param>        
        /// <param name="type">Injected <see cref="string"/></param>
        /// <param name="size">Injected <see cref="float?"/></param>
        /// <param name="data">Injected <see cref="string"/></param>
        Task AddAsFileNameActivity(int driveitemid, string @filename, string @type, float? @size, string @data);

        /// <summary>
        /// Adds Activity
        /// </summary>
        /// <param name="driveitemid">Injected <see cref="DriveItem"/></param>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        Task AddAsNameActivity(int driveitemid, string @name, string @extension);

        /// <summary>
        /// Checks File Name
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        Task<bool> CheckFileName(string @filename, int? @parent, int @userid);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>       
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        Task<bool> CheckName(string @name, int @id, int? @parent, int @userid, string @extension = null);
        
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
