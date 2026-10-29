using Hyperdrive.Storage.Domain.Dtos;
using Hyperdrive.Storage.Domain.Entities;

namespace Hyperdrive.Storage.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IDriveItemManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IDriveItemManager : IBaseManager
    {
        /// <summary>
        /// Finds Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="Guid"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public Task<DriveItem> FindDriveItemById(Guid? @id);

        /// <summary>
        /// Finds Drive Item By FileName
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>       
        /// <param name="userexternalid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public Task<DriveItem> FindDriveItemByFileName(string @filename, Guid? @parentid, int @userexternalid);

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
        /// <param name="userexternalid">Injected <see cref="int"/></param>
        /// <param name="parentid">Injected <see cref="Guid?"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        public Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @userexternalid, Guid? parentid);

        /// <summary>
        /// Finds Paginated Shared Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="userexternalid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        public Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemWithApplicationUserId(int @index, int @size, int @userexternalid);

        /// <summary>
        /// Adds Drive Item
        /// </summary>
        /// <param name="parentid">Injected <see cref="Guid?"/></param>
        /// <param name="folder">Injected <see cref="bool"/></param>
        /// <param name="userexternalid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public Task<DriveItem> AddDriveItem(Guid? parentid, bool @folder, int @userexternalid);

        /// <summary>
        /// Adds Shared With
        /// </summary>
        /// <param name="users">Injected <see cref="List{int}"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        public Task AddSharedWith(IList<int> @users, DriveItem @entity);

        /// <summary>
        /// Checks File Name
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parentid">Injected <see cref="Guid?"/></param>
        /// <param name="userexternalid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> CheckFileName(string @filename, Guid? @parentid, int @userexternalid);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>       
        /// <param name="id">Injected <see cref="Guid"/></param>
        /// <param name="parentid">Injected <see cref="Guid?"/></param>
        /// <param name="userexternalid">Injected <see cref="int"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> CheckName(string @name, Guid @id, Guid? @parentid, int @userexternalid, string @extension = null);

        /// <summary>
        /// Reloads Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="Guid"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemDto}"/></returns>
        public Task<DriveItemDto> ReloadDriveItemById(Guid @id);
    }
}
