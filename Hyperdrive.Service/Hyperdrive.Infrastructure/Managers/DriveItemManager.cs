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
        public async Task<DriveItem> FindDriveItemById(int @id)
        {
            DriveItem @archive = await Context.DriveItems
                 .TagWith("FindDriveItemById")
                 .Include(x => x.Activity)
                 .Include(x => x.SharedWith)
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

        public async Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @id)
        {
            PageDto<DriveItemDto> @page = new()
            {
                Length = await Context.DriveItems.TagWith("CountAllDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.By)
                    .Where(x => x.By.Id == @id)
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = (await Context.DriveItems
                   .TagWith("FindPaginatedDriveItemByApplicationUserId")
                   .AsSplitQuery()
                   .AsNoTracking()
                   .Include(x => x.Activity)
                   .Include(x => x.By)
                   .Where(x => x.By.Id == @id)
                   .Skip(@index * @size)
                   .Take(@size)
                   .Select(x=> x.ToDto())
                   .ToListAsync())
            };

            return @page;
        }

        public async Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemByApplicationUserId(int @index, int @size, int @id)
        {
            PageDto<DriveItemDto> @page = new()
            {
                Length = await Context.ApplicationUserDriveItems.TagWith("CountAllSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.DriveItem)
                    .Where(x => x.ApplicationUser.Id == @id)
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = await Context.ApplicationUserDriveItems
                    .TagWith("FindPaginatedSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.DriveItem)
                    .Where(x => x.ApplicationUser.Id == @id)
                    .Skip(@index * @size)
                    .Take(@size)
                    .Select(x => x.DriveItem.ToDto())
                    .ToListAsync()
            };

            return @page;
        }

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

        public async Task<DriveItem> AddDriveItem(DriveItem @entity)
        {
            await CheckName(@entity.Name, entity.Parent.Id);

            await Context.DriveItems.AddAsync(entity);

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

        public async Task AddApplicationUserDriveItem(List<ApplicationUserDriveItem> @userDriveItems)
        {
            await Context.ApplicationUserDriveItems.AddRangeAsync(@userDriveItems);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(ApplicationUserDriveItem)
                              + "s with Ids "
                              + string.Concat(@userDriveItems.Select(x=> x.Id))
                              + " were added at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        public async Task AddDriveItemVersion(DriveItemVersion @entity)
        {
            await Context.DriveItemVersions.AddAsync(@entity);

            await Context.SaveChangesAsync();
            
            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItemVersion)
                              + " with Id "
                              + @entity.Id
                              + " was added at "
                              + DateTime.UtcNow.ToShortTimeString();
            
            @logger.LogInformation(@logData);
        }

        public async Task<DriveItem> ChangeName(string @name, int @id, int @parent)
        {
            await CheckName(@name, @id, @parent);
            
            DriveItem @entity = await FindDriveItemById(@id);

            @entity.Name = @name?.Trim();
            
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

        public async Task<DriveItem> CheckName(string @name, int @parent)
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

        public async Task<DriveItem> CheckName(string @name, int @id, int @parent)
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
    }
}
