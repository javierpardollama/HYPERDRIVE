using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Profiles;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Hyperdrive.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hyperdrive.Infrastructure.Managers
{
    /// <summary>
    /// Represents a <see cref="DriveItemManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemManager"/>
    /// </summary>    
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="logger">Injected <see cref="ILogger"/></param>
    public class DriveItemManager(
        IApplicationContext context,
        ILogger<DriveItemManager> logger) : BaseManager(context), IDriveItemManager
    {
        /// <summary>
        /// Finds Drive Item By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> FindDriveItemById(int? @id)
        {
            DriveItem @archive = await Context.DriveItems
                .TagWith("FindDriveItemById")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (@archive is null)
            {
                // Log
                string @logData = nameof(DriveItem)                                
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)                                          
                                           + " does not exist");
            }

            return @archive;
        }

        /// <summary>
        /// Finds Drive Item By FileName
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>       
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> FindDriveItemByFileName(string @filename, int? parentid, int userid)
        {
            var @expr1 = (Expression<Func<DriveItemVersion, bool>>)(x => x.DriveItem.ById == @userid);
            var @expr2 = (Expression<Func<DriveItemVersion, bool>>)(x => x.FileName == @filename.Trim());
            var @expr3 = (Expression<Func<DriveItemVersion, bool>>)(x => parentid == null || x.DriveItem.ParentId == @parentid);
            var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3);

            var @archive = await Context.DriveItemVersions
                .TagWith("FindDriveItemByFileName")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.DriveItem)
                .Where(@comboexp)
                .Select(x => x.DriveItem)
                .FirstOrDefaultAsync();

            if (@archive is null)
            {
                // Log
                string @logData = nameof(DriveItem)                                
                                  + " was already found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)                                          
                                           + " already exists");
            }

            return @archive;
        }

        /// <summary>
        /// Removes Drive Item
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task RemoveDriveItem(DriveItem @entity)
        {
            Context.DriveItems.Remove(@entity);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItem)                             
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
        public async Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @userid, int? parentid)
        {
            var @expr1 = (Expression<Func<DriveItem, bool>>)(x => x.ById == @userid);
            var @expr2 = (Expression<Func<DriveItem, bool>>)(x => parentid == null || x.ParentId == @parentid);
            var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2);

            PageDto<DriveItemDto> @page = new()
            {
                Length = await Context.DriveItems.TagWith("CountAllDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.By)
                    .Include(x => x.Parent)
                    .Where(@comboexp)
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = (await Context.DriveItems
                    .TagWith("FindPaginatedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.By)
                    .Include(x => x.Parent)
                    .Where(@comboexp)
                    .Skip(@index * @size)
                    .Take(@size)
                    .Select(x => x.ToDto())
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
        public async Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemWithApplicationUserId(int @index,
                                                                                                   int @size,
                                                                                                   int @userid)
        {
            PageDto<DriveItemDto> @page = new()
            {
                Length = await Context.ApplicationUserDriveItems.TagWith("CountAllSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Where(x => x.UserId == @userid)
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = await Context.ApplicationUserDriveItems
                    .TagWith("FindPaginatedSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.DriveItem.Activity)
                    .Where(x => x.UserId == @userid)
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
                .Where(x => x.DriveItemId == @id)
                .Select(x => x.ToDto())
                .ToListAsync();

            return @versions;
        }

        /// <summary>
        /// Adds Drive Item
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <param name="folder">Injected <see cref="bool"/></param>
        /// <param name="byid">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> AddDriveItem(string @filename, int? parentid, bool folder, int @byid)
        {
            var @entity = new DriveItem()
            {
                Folder = @folder,
                ParentId = @parentid,
                ById = @byid
            };

            await Context.DriveItems.AddAsync(@entity);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItem)                             
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @entity;
        }

        /// <summary>
        /// Adds Shared With
        /// </summary>
        /// <param name="users">Injected <see cref="IList{ApplicationUser}"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        public async Task AddSharedWith(IList<ApplicationUser> @users, DriveItem @entity)
        {
            var @userDriveItems = users.Select(@user => new ApplicationUserDriveItem()
            {
                DriveItem = @entity,
                User = @user
            }).ToList();

            await Context.ApplicationUserDriveItems.AddRangeAsync(@userDriveItems);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(ApplicationUserDriveItem)                             
                              + " were added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Adds Drive Item Version
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
        /// <param name="filename">Injected <see cref="string"/></param>       
        /// <param name="type">Injected <see cref="string"/></param>
        /// <param name="size">Injected <see cref="float?"/></param>
        ///  <param name="data">Injected <see cref="string"/></param>
        public async Task AddAsFileNameActivity(int driveitemid, string @filename, string @type, float? @size, string @data)
        {
            DriveItemVersion @version = new()
            {
                FileName = @filename.Trim(),
                NormalizedFileName = @filename.Trim().ToUpper(),
                Name = Path.GetFileNameWithoutExtension(@filename.Trim()),
                NormalizedName = Path.GetFileNameWithoutExtension(@filename.Trim()).ToUpper(),
                Extension = Path.GetExtension(@filename.Trim()),
                NormalizedExtension = Path.GetExtension(@filename.Trim()).ToUpper(),
                Type = @type,
                Size = @size,
                Data = !string.IsNullOrWhiteSpace(@data) ? Convert.FromBase64String(@data) : null,
                DriveItemId = driveitemid
            };

            await Context.DriveItemVersions.AddAsync(@version);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItemVersion)                             
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Adds Activity
        /// </summary>
        /// <param name="driveitemid">Injected <see cref="int"/></param>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        public async Task AddAsNameActivity(int driveitemid, string @name, string @extension)
        {
            DriveItemVersion @version = new()
            {
                Extension = @extension.Trim(),
                NormalizedExtension = @extension.Trim().ToUpper(),
                FileName = $"{@name?.Trim()}.{extension.Trim()}",
                NormalizedFileName = $"{@name?.Trim().ToUpper()}.{extension.Trim().ToUpper()}",
                Name = @name.Trim(),
                NormalizedName = @name.Trim().ToUpper(),
                DriveItemId = driveitemid
            };

            await Context.DriveItemVersions.AddAsync(@version);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItemVersion)                             
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Checks File Name
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public async Task<bool> CheckFileName(string @filename, int? @parentid, int @userid)
        {
            var @expr1 = (Expression<Func<DriveItemVersion, bool>>)(x => x.FileName == @filename.Trim());
            var @expr2 = (Expression<Func<DriveItemVersion, bool>>)(x => @parentid == null || x.DriveItem.ParentId == @parentid);
            var @expr3 = (Expression<Func<DriveItemVersion, bool>>)(x => x.DriveItem.ById == @userid);
            var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3);

            var @found = await Context.DriveItemVersions
                .TagWith("CheckFileName")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.DriveItem)
                .Where(@comboexp)
                .Select(x => x.DriveItem)
                .AnyAsync();

            if (@found)
            {
                // Log
                string @logData = nameof(DriveItem)                                 
                                  + " was already found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)                                          
                                           + " already exists");
            }

            return @found;
        }



        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="parentid">Injected <see cref="int?"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<bool> CheckName(string @name, int @id, int? @parentid, int userid, string @extension = null)
        {
            var @expr1 = (Expression<Func<DriveItemVersion, bool>>)(x => x.Name == @name.Trim());
            var @expr2 = (Expression<Func<DriveItemVersion, bool>>)(x => @extension == null || x.Extension == @extension.Trim());
            var @expr3 = (Expression<Func<DriveItemVersion, bool>>)(x => parentid == null || x.DriveItem.ParentId == @parentid);
            var @expr4 = (Expression<Func<DriveItemVersion, bool>>)(x => x.DriveItem.Id != @id);
            var @expr5 = (Expression<Func<DriveItemVersion, bool>>)(x => x.DriveItem.ById == @userid);
            var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3, @expr4, @expr5);

            var @found = await Context.DriveItemVersions
                .TagWith("CheckName")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.DriveItem)
                .Include(x => x.DriveItem)
                .Where(@comboexp)
                .AnyAsync();

            if (@found)
            {
                // Log
                string @logData = nameof(DriveItem)                                 
                                  + " was already found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)                                          
                                           + " already exists");
            }

            return @found;
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
                .Select(x => x.ToBinary())
                .FirstOrDefaultAsync();

            if (@archive is null)
            {
                // Log
                string @logData = nameof(DriveItem)                                
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)                                          
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
                .Include(x => x.By)
                .Include(x => x.SharedWith)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == @id)
                .Select(x => x.ToDto())
                .FirstOrDefaultAsync();

            if (@archive is null)
            {
                // Log
                string @logData = nameof(DriveItem)                                
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