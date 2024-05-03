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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="ApplicationRoleService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IApplicationRoleService"/>
    /// </summary>  
    /// <param name="mapper">Injected <see cref="IMapper"/></param>
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="logger">Injected <see cref="ILogger{ApplicationRoleService}"/></param>
    public class ApplicationRoleService(IMapper @mapper,
                                  IApplicationContext @context,
                                  ILogger<ApplicationRoleService> @logger) : BaseService(@context, @mapper, @logger), IApplicationRoleService
    {

        /// <summary>
        /// Adds Application Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationRole}"/></returns>
        public async Task<ViewApplicationRole> AddApplicationRole(AddApplicationRole @viewModel)
        {
            await CheckName(@viewModel);

            ApplicationRole @applicationRole = new()
            {
                Name = @viewModel.Name?.Trim(),
                NormalizedName = @viewModel.Name?.Trim().ToUpper(),
                ConcurrencyStamp = DateTime.Now.ToBinary().ToString(),
                ImageUri = @viewModel.ImageUri.Trim(),
            };

            await Context.Roles.AddAsync(@applicationRole);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(@applicationRole)
                + " with Id "
                + @applicationRole.Id
                + " was added at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteInsertItemLog(@logData);

            return Mapper.Map<ViewApplicationRole>(@applicationRole);
        }

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        public async Task<ApplicationRole> CheckName(AddApplicationRole @viewModel)
        {
            ApplicationRole @applicationRole = await Context.Roles
                .AsNoTracking()
                .TagWith("CheckName")
                .FirstOrDefaultAsync(x => x.Name == @viewModel.Name.Trim());

            if (@applicationRole != null)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Name "
                    + @applicationRole.Name
                    + " was already found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemFoundLog(@logData);

                throw new Exception(nameof(@applicationRole)
                    + " with Name "
                    + viewModel.Name
                    + " already exists");
            }

            return @applicationRole;
        }

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        public async Task<ApplicationRole> CheckName(UpdateApplicationRole @viewModel)
        {
            ApplicationRole @applicationRole = await Context.Roles
                 .AsNoTracking()
                 .AsSplitQuery()
                 .TagWith("CheckName")
                 .FirstOrDefaultAsync(x => x.Name == @viewModel.Name.Trim() && x.Id != @viewModel.Id);

            if (@applicationRole != null)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Name "
                    + applicationRole.Name
                    + " was already found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemFoundLog(@logData);

                throw new Exception(nameof(@applicationRole)
                    + " with Name "
                    + viewModel.Name
                    + " already exists");
            }

            return @applicationRole;
        }

        /// <summary>
        /// Finds All Application Role
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{ViewApplicationRole}}"/></returns>
        public async Task<ICollection<ViewApplicationRole>> FindAllApplicationRole()
        {
            ICollection<ApplicationRole> @applicationRoles = await Context.Roles
                .TagWith("FindAllApplicationRole")
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            return Mapper.Map<ICollection<ViewApplicationRole>>(@applicationRoles);
        }

        /// <summary>
        /// Finds Paginated Application Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="FilterPageApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ViewPage{ViewApplicationRole}}"/></returns>
        public async Task<ViewPage<ViewApplicationRole>> FindPaginatedApplicationRole(FilterPageApplicationRole @viewModel)
        {
            ViewPage<ViewApplicationRole> @page = new()
            {
                Length = await Context.Roles
                    .TagWith("CountAllApplicationRole")
                    .AsNoTracking()
                    .AsSplitQuery()
                    .CountAsync(),
                Index = @viewModel.Index,
                Size = @viewModel.Size,
                Items = Mapper.Map<IList<ViewApplicationRole>>(await Context.Roles
               .TagWith("FindPaginatedApplicationRole")
               .AsNoTracking()
               .AsSplitQuery()
               .Skip(@viewModel.Index * @viewModel.Size)
               .Take(@viewModel.Size)
               .ToListAsync())
            };

            return @page;
        }

        /// <summary>
        /// Finds Application Role By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        public async Task<ApplicationRole> FindApplicationRoleById(int @id)
        {
            ApplicationRole @applicationRole = await Context.Roles.
                TagWith("FindApplicationRoleById")
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationRole == null)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Id "
                    + @id
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(@logData);

                throw new Exception(nameof(@applicationRole)
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return @applicationRole;
        }

        /// <summary>
        /// Removes Application Role By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task RemoveApplicationRoleById(int @id)
        {
            ApplicationRole @applicationRole = await FindApplicationRoleById(@id);

            Context.Roles.Remove(@applicationRole);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(@applicationRole)
                + " with Id "
                + @applicationRole.Id
                + " was removed at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteDeleteItemLog(@logData);
        }

        /// <summary>
        /// Updates Application Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationRole}"/></returns>
        public async Task<ViewApplicationRole> UpdateApplicationRole(UpdateApplicationRole @viewModel)
        {
            await CheckName(@viewModel);

            ApplicationRole @applicationRole = await FindApplicationRoleById(@viewModel.Id);

            @applicationRole.Name = @viewModel.Name?.Trim();
            @applicationRole.NormalizedName = @viewModel.Name?.Trim().ToUpper();
            @applicationRole.ImageUri = @viewModel.ImageUri.Trim();

            Context.Roles.Update(@applicationRole);

            await Context.SaveChangesAsync();

            // Log
            string @logData = nameof(@applicationRole)
                + " with Id "
                + @applicationRole.Id
                + " was modified at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteUpdateItemLog(@logData);

            return Mapper.Map<ViewApplicationRole>(@applicationRole);
        }
    }
}
