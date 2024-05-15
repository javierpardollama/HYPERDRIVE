using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="ArchiveService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IArchiveService"/>
    /// </summary>    
    /// <param name="mapper">Injected <see cref="IMapper"/></param>
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="logger">Injected <see cref="ILogger{ArchiveService}"/></param>
    public class ArchiveService(UserManager<ApplicationUser> userManager,
                          IApplicationContext context,
                          IMapper mapper,
                          ILogger<ArchiveService> logger) : BaseService(context, mapper, logger), IArchiveService
    {
        public async Task<Archive> FindArchiveById(int @id)
        {
            Archive @archive = await Context.Archives
                 .TagWith("FindArchiveById")
                 .Include(x => x.ArchiveVersions)
                 .Include(x => x.ApplicationUserArchives)
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (@archive == null)
            {
                // Log
                string @logData = nameof(@archive)
                    + " with Id "
                    + id
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(@logData);

                throw new Exception(nameof(@archive)
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return @archive;
        }

        public async Task RemoveArchiveById(int @id)
        {
            Archive @archive = await FindArchiveById(@id);

            Context.Archives.Remove(@archive);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(@archive)
                + " with Id "
                + archive.Id
                + " was removed at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteDeleteItemLog(@logData);
        }

        public async Task<IList<ViewArchive>> FindAllArchive()
        {
            ICollection<Archive> @archives = await Context.Archives
                .TagWith("FindAllArchive")
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.ArchiveVersions)
                .Include(x => x.By)
                .ToListAsync();

            return Mapper.Map<IList<ViewArchive>>(@archives);
        }

        public async Task<ViewPage<ViewArchive>> FindPaginatedArchiveByApplicationUserId(FilterPageArchive @viewModel)
        {
            ViewPage<ViewArchive> @page = new()
            {
                Length = await Context.Archives.TagWith("CountAllArchiveByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.By)
                    .Where(x => x.By.Id == @viewModel.ApplicationUserId)
                    .CountAsync(),
                Index = @viewModel.Index,
                Size = @viewModel.Size,
                Items = Mapper.Map<IList<ViewArchive>>(await Context.Archives
                   .TagWith("FindPaginatedArchiveByApplicationUserId")
                   .AsSplitQuery()
                   .AsNoTracking()
                   .Include(x => x.ArchiveVersions)
                   .Include(x => x.By)
                   .Where(x => x.By.Id == @viewModel.ApplicationUserId)
                   .Skip(@viewModel.Index * @viewModel.Size)
                   .Take(@viewModel.Size)
                   .ToListAsync())
            };

            return @page;
        }

        public async Task<ViewPage<ViewArchive>> FindPaginatedSharedArchiveByApplicationUserId(FilterPageArchive @viewModel)
        {
            ViewPage<ViewArchive> @page = new()
            {
                Length = await Context.ApplicationUserArchives.TagWith("CountAllSharedArchiveByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Archive)
                    .Where(x => x.ApplicationUser.Id == @viewModel.ApplicationUserId)
                    .CountAsync(),
                Index = @viewModel.Index,
                Size = @viewModel.Size,
                Items = Mapper.Map<IList<ViewArchive>>(await Context.ApplicationUserArchives
                    .TagWith("FindPaginatedSharedArchiveByApplicationUserId")
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Archive)
                    .Where(x => x.ApplicationUser.Id == @viewModel.ApplicationUserId)
                    .Skip(@viewModel.Index * @viewModel.Size)
                    .Take(@viewModel.Size)
                    .Select(x => x.Archive)
                    .ToListAsync())
            };

            return @page;
        }

        public async Task<IList<ViewArchiveVersion>> FindAllArchiveVersionByArchiveId(int @id)
        {
            ICollection<ArchiveVersion> @versions = await Context.ArchiveVersions
               .TagWith("FindAllArchiveVersionByArchiveId")
               .AsSplitQuery()
               .AsNoTracking()
               .Include(x => x.Archive)
               .ThenInclude(x => x.By)
               .Where(x => x.Archive.Id == @id)
               .ToListAsync();

            return Mapper.Map<IList<ViewArchiveVersion>>(@versions);
        }

        public async Task<ApplicationUser> FindApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await userManager.Users
                .TagWith("FindApplicationUserById")                
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationUser == null)
            {
                // Log
                string logData = nameof(@applicationUser)
                    + " with Id "
                    + id
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(@logData);

                throw new Exception(nameof(@applicationUser)
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return @applicationUser;
        }

        public async Task<ViewArchive> AddArchive(AddArchive @viewModel)
        {
            await CheckName(@viewModel);

            Archive @archive = new()
            {
                Folder = @viewModel.Folder,
                Locked = @viewModel.Locked,               
                By = await FindApplicationUserById(@viewModel.ApplicationUserId),
                ApplicationUserArchives = [],
                ArchiveVersions = []
            };

            await Context.Archives.AddAsync(@archive);

            AddApplicationUserArchive(@viewModel, @archive);

            AddArchiveVersion(@viewModel, @archive);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(@archive)
                + " with Id "
                + @archive.Id
                + " was added at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteInsertItemLog(@logData);

            return Mapper.Map<ViewArchive>(@archive);
        }

        public void AddApplicationUserArchive(AddArchive @viewModel,
                                             Archive @entity)
        {
            @viewModel.ApplicationUsersId.AsQueryable().ToList().ForEach(async x =>
            {
                ApplicationUser @applicationUser = await FindApplicationUserById(x);

                ApplicationUserArchive @applicationUserArchive = new()
                {
                    Archive = @entity,
                    ApplicationUser = @applicationUser,
                };

                @entity.ApplicationUserArchives.Add(@applicationUserArchive);
            });
        }

        public void AddArchiveVersion(AddArchive @viewModel, Archive @entity)
        {
            ArchiveVersion @archiveVersion = new()
            {
                Archive = @entity,
                Name = @viewModel.Name.Trim(),
                Data = @viewModel.Data,
                Size = @viewModel.Size,
                Type = @viewModel.Type
            };

            @entity.ArchiveVersions.Add(@archiveVersion);
        }

        public async Task<ViewArchive> UpdateArchive(UpdateArchive @viewModel)
        {
            await CheckName(@viewModel);

            Archive @archive = await FindArchiveById(@viewModel.Id);           
            @archive.Folder = @viewModel.Folder;
            @archive.Locked = @viewModel.Locked;          
            @archive.By = await FindApplicationUserById(@viewModel.ApplicationUserId);

            Context.Archives.Update(@archive);

            UpdateApplicationUserArchive(@viewModel, @archive);

            UpdateArchiveVersion(@viewModel, @archive);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(@archive)
                + " with Id "
                + @archive.Id
                + " was modified at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteUpdateItemLog(@logData);

            return Mapper.Map<ViewArchive>(@archive);
        }

        public void UpdateApplicationUserArchive(UpdateArchive @viewModel, Archive @entity)
        {
            @viewModel.ApplicationUsersId.AsQueryable().ToList().ForEach(async x =>
            {
                ApplicationUser @applicationUser = await FindApplicationUserById(x);

                ApplicationUserArchive @applicationUserArchive = new()
                {
                    Archive = @entity,
                    ApplicationUser = @applicationUser,
                };

                @entity.ApplicationUserArchives.Add(@applicationUserArchive);
            });
        }

        public void UpdateArchiveVersion(UpdateArchive @viewModel, Archive @entity)
        {
            ArchiveVersion @archiveVersion = new()
            {
                Archive = @entity,
                Name = @viewModel.Name.Trim(),
                Data = @viewModel.Data,
                Size = @viewModel.Size,
                Type = @viewModel.Type
            };

            @entity.ArchiveVersions.Add(@archiveVersion);
        }

        public async Task<Archive> CheckName(AddArchive @viewModel)
        {
            Archive @archive = await Context.Archives
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(x => x.ArchiveVersions.LastOrDefault().Name == @viewModel.Name.Trim());

            if (@archive != null)
            {
                // Log
                string @logData = nameof(@archive)
                    + " with Name "
                    + @archive.ArchiveVersions.LastOrDefault().Name
                    + " was already found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemFoundLog(@logData);

                throw new Exception(nameof(@archive)
                    + " with Name "
                    + @viewModel.Name
                    + " already exists");
            }

            return @archive;
        }


        public async Task<Archive> CheckName(UpdateArchive @viewModel)
        {
            Archive @archive = await Context.Archives
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(x => x.ArchiveVersions.LastOrDefault().Name == @viewModel.Name.Trim() && x.Id != @viewModel.Id);

            if (@archive != null)
            {
                // Log
                string @logData = nameof(@archive)
                    + " with Name "
                    + @archive.ArchiveVersions.LastOrDefault().Name
                    + " was already found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemFoundLog(@logData);

                throw new Exception(nameof(@archive)
                    + " with Name "
                    + @viewModel.Name
                    + " already exists");
            }

            return @archive;
        }
    }
}
