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
        /// Finds Drive Item By FileName
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>       
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> FindDriveItemByFileName(string @filename, int? parent, int userid)
        {
            var @expr1 = (Expression<Func<DriveItemVersion, bool>>)(x => x.FileName == @filename.Trim());
            var @expr2 = (Expression<Func<DriveItemVersion, bool>>)(x => parent == null || x.DriveItem.Parent.Id == @parent);
            var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2);          

            var @archive = await Context.DriveItemVersions
                .TagWith("FindDriveItemByFileName")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.DriveItem.Parent)
                .Include(x => x.DriveItem.By)
                .Where(@comboexp)
                .Select(x=> x.DriveItem)
                .FirstOrDefaultAsync();

            if (@archive is null)
            {
                // Log
                string @logData = nameof(DriveItem)
                                  + " with FileName "
                                  + @filename
                                  + " was already found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                                           + " with FileName "
                                           + @filename
                                           + " already exists");
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
            var @expr1 = (Expression<Func<DriveItem, bool>>)(x => x.By.Id == @userid);
            var @expr2 = (Expression<Func<DriveItem, bool>>)(x => parent == null || x.Parent.Id == @parent);           
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
                    .Include(x => x.ApplicationUser)                    
                    .Where(x => x.ApplicationUser.Id == @userid)
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = await Context.ApplicationUserDriveItems
                    .TagWith("FindPaginatedSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.DriveItem.Activity)
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
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <param name="folder">Injected <see cref="bool"/></param>
        /// <param name="by">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<DriveItem> AddDriveItem(string @filename, int? parent, bool folder, ApplicationUser @by)
        {
            await CheckFileName(@filename, @parent, @by.Id);

            var @entity = new DriveItem()
            {               
                Folder = @folder,
                Parent = @parent is not null ? await FindDriveItemById(@parent) : null,
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
        /// Adds Shared With
        /// </summary>
        /// <param name="users">Injected <see cref="IList{ApplicationUser}"/></param>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        public async Task AddSharedWith(IList<ApplicationUser> @users, DriveItem @entity)
        {
            var @userDriveItems = users.Select(@user => new ApplicationUserDriveItem()
            {
                DriveItem = @entity,
                ApplicationUser = @user
            }).ToList();

            await Context.ApplicationUserDriveItems.AddRangeAsync(@userDriveItems);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(ApplicationUserDriveItem)
                              + "s with Ids "
                              + string.Join(",", @userDriveItems.Select(x => x.Id))
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
        public async Task AddAsFileNameActivity(DriveItem @entity, string @filename, string @type, float? @size, string @data)
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
                Data = Convert.FromBase64String(@data),
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
        /// Adds Activity
        /// </summary>
        /// <param name="entity">Injected <see cref="DriveItem"/></param>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="extension">Injected <see cref="string"/></param>
        public async Task AddAsNameActivity(DriveItem @entity, string @name, string @extension) 
        {        
            DriveItemVersion @version = new()
            {
                Extension = @extension.Trim(),
                NormalizedExtension = @extension.Trim().ToUpper(),
                FileName = $"{@name?.Trim()}.{extension.Trim()}",
                NormalizedFileName = $"{@name?.Trim().ToUpper()}.{extension.Trim().ToUpper()}",
                Name = @name.Trim(),
                NormalizedName = @name.Trim().ToUpper(),    
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
        /// Checks File Name
        /// </summary>
        /// <param name="filename">Injected <see cref="string"/></param>
        /// <param name="parent">Injected <see cref="int"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public async Task<bool> CheckFileName(string @filename, int? @parent, int @userid)
        {
            var @expr1 = (Expression<Func<DriveItemVersion, bool>>)(x => x.FileName == @filename.Trim());           
            var @expr2 = (Expression<Func<DriveItemVersion, bool>>)(x => parent == null || x.DriveItem.Parent.Id == @parent);            
            var @expr3 = (Expression<Func<DriveItemVersion, bool>>)(x => x.DriveItem.By.Id == @userid);
            var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3);          

            var @found = await Context.DriveItemVersions
                .TagWith("CheckFileName")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.DriveItem.Parent)
                .Include(x => x.DriveItem.By)
                .Where(@comboexp)
                .Select(x=> x.DriveItem)
                .AnyAsync();

            if (@found)
            {
                // Log
                string @logData = nameof(DriveItem)
                                  + " with FileName "
                                  + @filename
                                  + " was already found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                                           + " with FileName "
                                           + @filename
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
        /// <param name="parent">Injected <see cref="int?"/></param>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
        public async Task<bool> CheckName(string @name, int @id, int? @parent, int userid, string @extension = null)
        {           
            var @expr1 = (Expression<Func<DriveItemVersion, bool>>)(x => x.Name == @name.Trim());
            var @expr2 = (Expression<Func<DriveItemVersion, bool>>)(x => @extension == null || x.Extension == @extension.Trim());
            var @expr3 = (Expression<Func<DriveItemVersion, bool>>)(x => parent == null || x.DriveItem.Parent.Id == @parent);
            var @expr4 = (Expression<Func<DriveItemVersion, bool>>)(x => x.DriveItem.Id != @id);
            var @expr5 = (Expression<Func<DriveItemVersion, bool>>)(x => x.DriveItem.By.Id == @userid);
            var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3, @expr4, @expr5);           

            var @found = await Context.DriveItemVersions
                .TagWith("CheckName")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.DriveItem.Parent)
                .Include(x => x.DriveItem.By)
                .Where(@comboexp)
                .AnyAsync();

            if (@found)
            {
                // Log
                string @logData = nameof(DriveItem)
                                  + " with Name "
                                  + name
                                  + " was already found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(DriveItem)
                                           + " with Name "
                                           + @name
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
                .Include(x => x.By)               
                .Include(x => x.SharedWith)
                .ThenInclude(x => x.ApplicationUser)
                .Where(x => x.Id == @id)
                .Select(x => x.ToDto())
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