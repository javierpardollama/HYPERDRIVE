using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Profiles;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Infrastructure.Managers
{
    /// <summary>
    /// Represents a <see cref="DriveItemManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemManager"/>
    /// </summary>    
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="logger">Injected <see cref="ILogger"/></param>
    public class DriveItemManager(IApplicationContext context,
                                  ILogger<DriveItemManager> logger) : BaseManager(context), IDriveItemManager
    {
        /// <summary>
        /// Finds Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> FindDriveItemById(int @id)
        {
            DriveItem @archive = await Context.DriveItems
                 .TagWith("FindDriveItemById")
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (@archive is null)
            {
                // Log
                string @logData = nameof(@archive)
                    + " with Id "
                    + id
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return @archive;
        }

        /// <summary>
        /// Removes Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task RemoveDriveItemById(int @id)
        {
            DriveItem @archive = await FindDriveItemById(@id);

            Context.DriveItems.Remove(@archive);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItem)
                + " with Id "
                + archive.Id
                + " was removed at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Finds Paginated Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        public async Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @userid, int? parent)
        {
            PageDto<DriveItemDto> @page = new()
            {
                Length = await Context.DriveItems.TagWith("CountAllDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.By)
                    .Include(x => x.Parent)
                    .Where(x => x.By.Id == userid && x.Parent.Id == @parent)
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = (await Context.DriveItems
                   .TagWith("FindPaginatedDriveItemByApplicationUserId")
                   .AsSplitQuery()
                   .AsNoTracking()
                   .Include(x => x.Activity)
                   .Include(x => x.By)
                   .Include(x => x.Parent)
                   .Where(x => x.By.Id == @userid && x.Parent.Id == @parent)
                   .Skip(@index * @size)
                   .Take(@size)
                   .Select(x=> x.ToDto())
                   .ToListAsync())
            };

            return @page;
        }

        /// <summary>
        /// Finds Paginated Shared Drive Item By Application User Id
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
        public async Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemWithApplicationUserId(int @index, int @size, int @userid)
        {
            PageDto<DriveItemDto> @page = new()
            {
                Length = await Context.ApplicationUserDriveItems.TagWith("CountAllSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.DriveItem)
                    .Where(x => x.ApplicationUser.Id == @userid)
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = await Context.ApplicationUserDriveItems
                    .TagWith("FindPaginatedSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.DriveItem)
                    .Where(x => x.ApplicationUser.Id == @userid)
                    .Skip(@index * @size)
                    .Take(@size)
                    .Select(x => x.DriveItem.ToDto())
                    .ToListAsync()
            };

            return @page;
        }

        /// <summary>
        /// Finds All Drive Item Version By Drive Item Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{IList{DriveItemVersionDto}}"/></returns>
        public async Task<IList<DriveItemVersionDto>> FindAllDriveItemVersionByDriveItemId(int @id)
        {
            IList<DriveItemVersionDto> @versions = await Context.DriveItemVersions
               .TagWith("FindAllDriveItemVersionByDriveItemId")
               .AsSplitQuery()
               .AsNoTracking()
               .Include(x => x.DriveItem)
               .ThenInclude(x => x.By)
               .Where(x => x.DriveItem.Id == @id)
               .Select(x => x.ToDto())
               .ToListAsync();

            return @versions;
        }

        /// <summary>
        /// Adds Drive Item
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <param name="folder">Injected <see cref="bool"/></param>
        /// <param name="by">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> AddDriveItem(string @name, int? parent, bool folder, ApplicationUser @by)
        {
            await CheckName(name, parent);
            
            var @entity = new DriveItem()
            {
                Name = name.Trim(),
                NormalizedName = name.Trim().ToUpper(),
                Folder = folder,
                By = @by
            };

            await Context.DriveItems.AddAsync(@entity);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItem)
                + " with Id "
                + @entity.Id
                + " was added at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @entity;
        }

        /// <summary>
        /// Adds Application User Drive Item
        /// </summary>
        /// <param name="userDriveItems">Injected <see cref="List{ApplicationUserDriveItem}"/></param>
        public async Task AddApplicationUserDriveItem(List<ApplicationUserDriveItem> @userDriveItems)
        {
            await Context.ApplicationUserDriveItems.AddRangeAsync(@userDriveItems);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(ApplicationUserDriveItem)
                              + "s with Ids "
                              + string.Join(",", @userDriveItems.Select(x=> x.Id))
                              + " were added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Adds Drive Item Version
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
        /// <param name="type">Injected <see cref="DriveItemVersion"/></param>
        /// <param name="size">Injected <see cref="DriveItemVersion"/></param>
        ///  <param name="data">Injected <see cref="DriveItemVersion"/></param>
        public async Task AddDriveItemVersion(DriveItem @entity, string @type, float? @size, string @data)
        {
            DriveItemVersion @version = new()
            {
                Type = @type,
                Size = @size,
                Data = @data,
                DriveItem = @entity
            };
            
            await Context.DriveItemVersions.AddAsync(@version);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItemVersion)
                              + " with Id "
                              + @version.Id
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();
            
            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> ChangeName(string @name, int @id, int? @parent)
        {
            await CheckName(@name, @id, @parent);
            
            DriveItem @entity = await FindDriveItemById(@id);

            @entity.Name = @name?.Trim();
            @entity.NormalizedName = @name?.Trim().ToUpper();
            
            Context.DriveItems.Update(@entity);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItem)
                + " with Id "
                + @entity.Id
                + " was modified at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @entity;
        }

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> CheckName(string @name, int? @parent)
        {
            DriveItem @archive = await Context.DriveItems
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(x => x.Name == @name.Trim() && x.Parent.Id == @parent);

            if (@archive is not null)
            {
                // Log
                string @logData = nameof(@archive)
                    + " with Name "
                    + @archive.Name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                    + " with Name "
                    + @name
                    + " already exists");
            }

            return @archive;
        }

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> CheckName(string @name, int @id, int? @parent)
        {
            DriveItem @archive = await Context.DriveItems
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(x => x.Name == @name.Trim() && x.Parent.Id == @parent && x.Id != @id);

            if (@archive is not null)
            {
                // Log
                string @logData = nameof(DriveItem)
                    + " with Name "
                    + @archive.Name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                    + " with Name "
                    + @name
                    + " already exists");
            }

            return @archive;
        }

        /// <summary>
        /// Finds Drive Item Binary By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
        public async Task<DriveItemBinaryDto> FindDriveItemBinaryById(int @id)
        {
            DriveItemBinaryDto @archive = await Context.DriveItems
                .TagWith("FindDriveItemBinaryById")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Activity)
                .Where(x => x.Id == @id)
                .Select(x=> x.ToBinary())
                .FirstOrDefaultAsync();

            if (@archive is null)
            {
                // Log
                string @logData = nameof(DriveItem)
                                  + " with Id "
                                  + id
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                                           + " with Id "
                                           + id
                                           + " does not exist");
            }

            return @archive;
        }

        /// <summary>
        /// Reloads Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItemDto}"/></returns>
        public async Task<DriveItemDto> ReloadDriveItemById(int @id)
        {
            DriveItemDto @archive = await Context.DriveItems
                .TagWith("ReloadDriveItemById")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Activity)
                .Include(x => x.SharedWith)
                .ThenInclude(x=> x.ApplicationUser)
                .Where(x=> x.Id == @id)
                .Select(x=> x.ToDto())
                .FirstOrDefaultAsync();

            if (@archive is null)
            {
                // Log
                string @logData = nameof(@archive)
                                  + " with Id "
                                  + id
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                                           + " with Id "
                                           + id
                                           + " does not exist");
            }

            return @archive;
        }
    }
}
