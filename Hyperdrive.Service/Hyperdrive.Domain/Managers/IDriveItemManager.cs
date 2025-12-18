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
        public Task<DriveItem> FindDriveItemById(int? @id);

        /// <summary>
        /// Finds Drive Item By FileName
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>       
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public Task<DriveItem> FindDriveItemByFileName(string @filename, int? @parentid, int @userid);

        /// <summary>
        /// Removes Drive Item
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public Task RemoveDriveItem(DriveItem @entity);

        /// <summary>
        /// Finds Paginated Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        public Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @userid, int? parentid);

        /// <summary>
        /// Finds Paginated Shared Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        public Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemWithApplicationUserId(int @index, int @size, int @userid);       

        /// <summary>
        /// Adds Drive Item
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>
        /// <param name="folder">Injected <see cref="bool"/></param>
        /// <param name="byid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public Task<DriveItem> AddDriveItem(string @filename, int? parentid, bool @folder, int @byid);

        /// <summary>
        /// Adds Shared With
        /// </summary>
        /// <param name="users">Injected <see cref="List{ApplicationUser}"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        public Task AddSharedWith(IList<ApplicationUser> @users, DriveItem @entity);

        /// <summary>
        /// Adds Activity
        /// </summary>
        /// <param name="driveitemid">Injected <see cref="int"/></param>
        /// <param name="filename">Injected <see cref="string"/></param>      
        /// <returns>Instance of <see cref="Task{DriveItemInfo}"/></returns>
        public Task<DriveItemInfo> AddAsFileNameInfo(int @driveitemid, string @filename);

        /// <summary>
        /// Adds Activity
        /// </summary>
        /// <param name="driveitemid">Injected <see cref="DriveItem"/></param>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemInfo}"/></returns>
        public Task<DriveItemInfo> AddAsNameInfo(int @driveitemid, string @name, string @extension);

        /// <summary>
        /// Adds Drive Item Content
        /// </summary>
        /// <param name="driveiteminfoid">Injected <see cref="int?"/></param>
        /// <param name="type">Injected <see cref="string"/></param>
        /// <param name="size">Injected <see cref="float"/></param>
        /// <param name="data">Injected <see cref="string"/></param>
        public Task AddAsFileContent(int? driveiteminfoid, string @type, float? @size, string @data);

        /// <summary>
        /// Checks File Name
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> CheckFileName(string @filename, int? @parentid, int @userid);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>       
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> CheckName(string @name, int @id, int? @parentid, int @userid, string @extension = null);       
        
        /// <summary>
        /// Reloads Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemDto}"/></returns>
        public Task<DriveItemDto> ReloadDriveItemById(int @id);
    }
}
