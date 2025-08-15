using AutoMapper;
using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Exceptions.Exceptions;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="DriveItemService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IDriveItemService"/>
    /// </summary>    
    /// <param name="mapper">Injected <see cref="IMapper"/></param>
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="logger">Injected <see cref="ILogger{DriveItemService}"/></param>
    public class DriveItemService(UserManager<ApplicationUser> userManager,
                                  IApplicationContext context,
                                  IMapper mapper,
                                  ILogger<DriveItemService> logger) : BaseService(context, mapper), IDriveItemService
    {
        public async Task<DriveItem> FindDriveItemById(int @id)
        {
            DriveItem @archive = await Context.DriveItems
                 .TagWith("FindDriveItemById")
                 .Include(x => x.DriveItemVersions)
                 .Include(x => x.ApplicationUserDriveItems)
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (@archive is null)
            {
                // Log
                string @logData = nameof(@archive)
                    + " with Id "
                    + id
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

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

            @logger.WriteDeleteItemLog(@logData);
        }

        public async Task<IList<ViewDriveItem>> FindAllDriveItem()
        {
            ICollection<DriveItem> @archives = await Context.DriveItems
                .TagWith("FindAllDriveItem")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.DriveItemVersions)
                .Include(x => x.By)
                .ToListAsync();

            return Mapper.Map<IList<ViewDriveItem>>(@archives);
        }

        public async Task<ViewPage<ViewDriveItem>> FindPaginatedDriveItemByApplicationUserId(FilterPageDriveItem @viewModel)
        {
            ViewPage<ViewDriveItem> @page = new()
            {
                Length = await Context.DriveItems.TagWith("CountAllDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.By)
                    .Where(x => x.By.Id == @viewModel.ApplicationUserId)
                    .CountAsync(),
                Index = @viewModel.Index,
                Size = @viewModel.Size,
                Items = Mapper.Map<IList<ViewDriveItem>>(await Context.DriveItems
                   .TagWith("FindPaginatedDriveItemByApplicationUserId")
                   .AsSplitQuery()
                   .AsNoTracking()
                   .Include(x => x.DriveItemVersions)
                   .Include(x => x.By)
                   .Where(x => x.By.Id == @viewModel.ApplicationUserId)
                   .Skip(@viewModel.Index * @viewModel.Size)
                   .Take(@viewModel.Size)
                   .ToListAsync())
            };

            return @page;
        }

        public async Task<ViewPage<ViewDriveItem>> FindPaginatedSharedDriveItemByApplicationUserId(FilterPageDriveItem @viewModel)
        {
            ViewPage<ViewDriveItem> @page = new()
            {
                Length = await Context.ApplicationUserDriveItems.TagWith("CountAllSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.DriveItem)
                    .Where(x => x.ApplicationUser.Id == @viewModel.ApplicationUserId)
                    .CountAsync(),
                Index = @viewModel.Index,
                Size = @viewModel.Size,
                Items = Mapper.Map<IList<ViewDriveItem>>(await Context.ApplicationUserDriveItems
                    .TagWith("FindPaginatedSharedDriveItemByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.DriveItem)
                    .Where(x => x.ApplicationUser.Id == @viewModel.ApplicationUserId)
                    .Skip(@viewModel.Index * @viewModel.Size)
                    .Take(@viewModel.Size)
                    .Select(x => x.DriveItem)
                    .ToListAsync())
            };

            return @page;
        }

        public async Task<IList<ViewDriveItemVersion>> FindAllDriveItemVersionByDriveItemId(int @id)
        {
            ICollection<DriveItemVersion> @versions = await Context.DriveItemVersions
               .TagWith("FindAllDriveItemVersionByDriveItemId")
               .AsSplitQuery()
               .AsNoTracking()
               .Include(x => x.DriveItem)
               .ThenInclude(x => x.By)
               .Where(x => x.DriveItem.Id == @id)
               .ToListAsync();

            return Mapper.Map<IList<ViewDriveItemVersion>>(@versions);
        }

        public async Task<ApplicationUser> FindApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await userManager.Users
                .TagWith("FindApplicationUserById")
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationUser is null)
            {
                // Log
                string logData = nameof(ApplicationUser)
                    + " with Id "
                    + id
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return @applicationUser;
        }

        public async Task<ViewDriveItem> AddDriveItem(AddDriveItem @viewModel)
        {
            await CheckName(@viewModel);

            DriveItem @archive = new()
            {
                Folder = @viewModel.Folder,
                Locked = @viewModel.Locked,
                By = await FindApplicationUserById(@viewModel.ApplicationUserId),
                ApplicationUserDriveItems = [],
                DriveItemVersions = []
            };

            await Context.DriveItems.AddAsync(@archive);

            AddApplicationUserDriveItem(@viewModel, @archive);

            AddDriveItemVersion(@viewModel, @archive);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItem)
                + " with Id "
                + @archive.Id
                + " was added at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.WriteInsertItemLog(@logData);

            return Mapper.Map<ViewDriveItem>(@archive);
        }

        public void AddApplicationUserDriveItem(AddDriveItem @viewModel,
                                             DriveItem @entity)
        {
            @viewModel.ApplicationUsersId.AsQueryable().ToList().ForEach(async x =>
            {
                ApplicationUser @applicationUser = await FindApplicationUserById(x);

                ApplicationUserDriveItem @applicationUserDriveItem = new()
                {
                    DriveItem = @entity,
                    ApplicationUser = @applicationUser,
                };

                @entity.ApplicationUserDriveItems.Add(@applicationUserDriveItem);
            });
        }

        public void AddDriveItemVersion(AddDriveItem @viewModel, DriveItem @entity)
        {
            DriveItemVersion @archiveVersion = new()
            {
                DriveItem = @entity,
                Data = @viewModel.Data,
                Size = @viewModel.Size,
                Type = @viewModel.Type
            };

            @entity.DriveItemVersions.Add(@archiveVersion);
        }

        public async Task<ViewDriveItem> UpdateDriveItem(UpdateDriveItem @viewModel)
        {
            await CheckName(@viewModel);

            DriveItem @archive = await FindDriveItemById(@viewModel.Id);
            @archive.Folder = @viewModel.Folder;
            @archive.Locked = @viewModel.Locked;
            @archive.Name = @viewModel.Name;
            @archive.By = await FindApplicationUserById(@viewModel.ApplicationUserId);

            Context.DriveItems.Update(@archive);

            UpdateApplicationUserDriveItem(@viewModel, @archive);

            UpdateDriveItemVersion(@viewModel, @archive);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(DriveItem)
                + " with Id "
                + @archive.Id
                + " was modified at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.WriteUpdateItemLog(@logData);

            return Mapper.Map<ViewDriveItem>(@archive);
        }

        public void UpdateApplicationUserDriveItem(UpdateDriveItem @viewModel, DriveItem @entity)
        {
            @viewModel.ApplicationUsersId.AsQueryable().ToList().ForEach(async x =>
            {
                ApplicationUser @applicationUser = await FindApplicationUserById(x);

                ApplicationUserDriveItem @applicationUserDriveItem = new()
                {
                    DriveItem = @entity,
                    ApplicationUser = @applicationUser,
                };

                @entity.ApplicationUserDriveItems.Add(@applicationUserDriveItem);
            });
        }

        public void UpdateDriveItemVersion(UpdateDriveItem @viewModel, DriveItem @entity)
        {
            DriveItemVersion @archiveVersion = new()
            {
                DriveItem = @entity,
                Data = @viewModel.Data,
                Size = @viewModel.Size,
                Type = @viewModel.Type
            };

            @entity.DriveItemVersions.Add(@archiveVersion);
        }

        public async Task<DriveItem> CheckName(AddDriveItem @viewModel)
        {
            DriveItem @archive = await Context.DriveItems
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(x => x.Name == @viewModel.Name.Trim());

            if (@archive is not null)
            {
                // Log
                string @logData = nameof(@archive)
                    + " with Name "
                    + @archive?.Name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemFoundLog(@logData);

                throw new ServiceException(nameof(DriveItem)
                    + " with Name "
                    + @viewModel.Name
                    + " already exists");
            }

            return @archive;
        }


        public async Task<DriveItem> CheckName(UpdateDriveItem @viewModel)
        {
            DriveItem @archive = await Context.DriveItems
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(x => x.Name == @viewModel.Name.Trim() && x.Id != @viewModel.Id);

            if (@archive is not null)
            {
                // Log
                string @logData = nameof(DriveItem)
                    + " with Name "
                    + @archive?.Name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemFoundLog(@logData);

                throw new ServiceException(nameof(DriveItem)
                    + " with Name "
                    + @viewModel.Name
                    + " already exists");
            }

            return @archive;
        }
    }
}
