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
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Services.Classes
{
    public class ArchiveService : BaseService, IArchiveService
    {
        private readonly UserManager<ApplicationUser> UserManager;

        public ArchiveService(UserManager<ApplicationUser> userManager,
                              IApplicationContext context,
                              IMapper mapper,
                              ILogger<ArchiveService> logger) : base(context, mapper, logger)
        {
            UserManager = userManager;
        }

        public async Task<Archive> FindArchiveById(int id)
        {
            Archive archive = await Context.Archive
                 .TagWith("FindArchiveById")
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (archive == null)
            {
                // Log
                string logData = archive.GetType().Name
                    + " with Id "
                    + id
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(logData);

                throw new Exception(archive.GetType().Name
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return archive;
        }

        public async Task RemoveArchiveById(int id)
        {
            Archive archive = await FindArchiveById(id);

            Context.Archive.Remove(archive);

            await Context.SaveChangesAsync();

            // Log
            string logData = archive.GetType().Name
                + " with Id "
                + archive.Id
                + " was removed at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteDeleteItemLog(logData);
        }

        public async Task<IList<ViewArchive>> FindAllArchive()
        {
            ICollection<Archive> archives = await Context.Archive
                .TagWith("FindAllArchive")
                .AsQueryable()
                .AsNoTracking()
                .Include(x => x.By)
                .ToListAsync();

            return Mapper.Map<IList<ViewArchive>>(archives);
        }

        public async Task<IList<ViewArchive>> FindAllArchiveByApplicationUserId(int id)
        {
            ICollection<Archive> archives = await Context.Archive
               .TagWith("FindAllArchiveByApplicationUserId")
               .AsQueryable()
               .AsNoTracking()
               .Include(x => x.By)
               .Where(x => x.By.Id == id)
               .ToListAsync();

            return Mapper.Map<IList<ViewArchive>>(archives);
        }

        public async Task<IList<ViewArchive>> FindAllSharedArchiveByApplicationUserId(int id)
        {
            ICollection<Archive> archives = await Context.ApplicationUserArchive
               .TagWith("FindAllSharedArchiveByApplicationUserId")
               .AsQueryable()
               .AsNoTracking()
               .Include(x => x.ApplicationUser)
               .Include(x => x.Archive)
               .ThenInclude(x => x.By)
               .Where(x => x.ApplicationUser.Id == id)
               .Select(x => x.Archive)
               .ToListAsync();

            return Mapper.Map<IList<ViewArchive>>(archives);
        }

        public async Task<IList<ViewArchiveVersion>> FindAllArchiveVersionByArchiveId(int id)
        {
            ICollection<ArchiveVersion> versions = await Context.ArchiveVersion
               .TagWith("FindAllArchiveVersionByArchiveId")
               .AsQueryable()
               .AsNoTracking()
               .Include(x => x.Archive)
               .ThenInclude(x => x.By)
               .Where(x => x.Archive.Id == id)
               .ToListAsync();

            return Mapper.Map<IList<ViewArchiveVersion>>(versions);
        }

        public async Task<ApplicationUser> FindApplicationUserByEmail(string email)
        {
            ApplicationUser applicationUser = await UserManager.Users
                .TagWith("FindApplicationUserByEmail")
                .AsQueryable()
                .Include(x => x.ApplicationUserTokens)
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (applicationUser == null)
            {
                // Log
                string logData = applicationUser.GetType().Name
                    + " with Email "
                    + email
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(logData);

                throw new Exception(applicationUser.GetType().Name
                    + " with Email "
                    + email
                    + " does not exist");
            }

            return applicationUser;
        }

        public async Task<ApplicationUser> FindApplicationUserById(int id)
        {
            ApplicationUser applicationUser = await UserManager.Users
                .TagWith("FindApplicationUserById")
                .AsQueryable()
                .Include(x => x.ApplicationUserTokens)
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (applicationUser == null)
            {
                // Log
                string logData = applicationUser.GetType().Name
                    + " with Id "
                    + id
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(logData);

                throw new Exception(applicationUser.GetType().Name
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return applicationUser;
        }

        public async Task<ViewArchive> AddArchive(AddArchive viewModel)
        {
            await CheckName(viewModel);

            Archive archive = new()
            {
                Name = viewModel.Name,
                By = await FindApplicationUserByEmail(viewModel.By.Email),
                ApplicationUserArchives = new List<ApplicationUserArchive>(),
                ArchiveVersions = new List<ArchiveVersion>()
            };

            await Context.Archive.AddAsync(archive);

            AddApplicationUserArchive(viewModel, archive);

            AddArchiveVersion(viewModel, archive);

            await Context.SaveChangesAsync();

            // Log
            string logData = archive.GetType().Name
                + " with Id "
                + archive.Id
                + " was added at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteInsertItemLog(logData);

            return Mapper.Map<ViewArchive>(archive);
        }

        public void AddApplicationUserArchive(AddArchive viewModel,
                                             Archive entity)
        {
            viewModel.ApplicationUsersId.AsQueryable().ToList().ForEach(async x =>
            {
                ApplicationUser applicationUser = await FindApplicationUserById(x);

                ApplicationUserArchive arenalPoblacion = new()
                {
                    Archive = entity,
                    ApplicationUser = applicationUser,
                };

                entity.ApplicationUserArchives.Add(arenalPoblacion);
            });
        }

        public void AddArchiveVersion(AddArchive viewModel, Archive entity)
        {
            ArchiveVersion archiveVersion = new()
            {
                Archive = entity,
                Data = viewModel.Data,
                Size = viewModel.Size,
                Type = viewModel.Type
            };

            entity.ArchiveVersions.Add(archiveVersion);
        }

        public async Task<ViewArchive> UpdateArchive(UpdateArchive viewModel)
        {
            await CheckName(viewModel);

            Archive archive = await FindArchiveById(viewModel.Id);
            archive.Name = viewModel.Name;
            archive.By = await FindApplicationUserByEmail(viewModel.By.Email);
            archive.ApplicationUserArchives = new List<ApplicationUserArchive>();

            Context.Archive.Update(archive);

            UpdateApplicationUserArchive(viewModel, archive);

            UpdateArchiveVersion(viewModel, archive);

            await Context.SaveChangesAsync();

            // Log
            string logData = archive.GetType().Name
                + " with Id "
                + archive.Id
                + " was modified at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteUpdateItemLog(logData);

            return Mapper.Map<ViewArchive>(archive);
        }

        public void UpdateApplicationUserArchive(UpdateArchive viewModel, Archive entity)
        {
            viewModel.ApplicationUsersId.AsQueryable().ToList().ForEach(async x =>
            {
                ApplicationUser applicationUser = await FindApplicationUserById(x);

                ApplicationUserArchive arenalPoblacion = new()
                {
                    Archive = entity,
                    ApplicationUser = applicationUser,
                };

                entity.ApplicationUserArchives.Add(arenalPoblacion);
            });
        }

        public void UpdateArchiveVersion(UpdateArchive viewModel, Archive entity)
        {
            ArchiveVersion archiveVersion = new()
            {
                Archive = entity,
                Data = viewModel.Data,
                Size = viewModel.Size,
                Type = viewModel.Type
            };

            entity.ArchiveVersions.Add(archiveVersion);
        }

        public async Task<Archive> CheckName(AddArchive viewModel)
        {
            Archive archive = await Context.Archive
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Name == viewModel.Name);

            if (archive != null)
            {
                // Log
                string logData = archive.GetType().Name
                    + " with Name "
                    + archive.Name
                    + " was already found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemFoundLog(logData);

                throw new Exception(archive.GetType().Name
                    + " with Name "
                    + viewModel.Name
                    + " already exists");
            }

            return archive;
        }


        public async Task<Archive> CheckName(UpdateArchive viewModel)
        {
            Archive archive = await Context.Archive
                 .TagWith("CheckName")
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Name == viewModel.Name && x.Id != viewModel.Id);

            if (archive != null)
            {
                // Log
                string logData = archive.GetType().Name
                    + " with Name "
                    + archive.Name
                    + " was already found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemFoundLog(logData);

                throw new Exception(archive.GetType().Name
                    + " with Name "
                    + viewModel.Name
                    + " already exists");
            }

            return archive;
        }
    }
}
