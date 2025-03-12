using AutoMapper;

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
    /// Represents a <see cref="ApplicationRoleService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IApplicationRoleService"/>
    /// </summary>  
    /// <param name="mapper">Injected <see cref="IMapper"/></param>
    /// <param name="logger">Injected <see cref="ILogger{ApplicationRoleService}"/></param>
    /// <param name="roleManager">Injected <see cref=" RoleManager{ApplicationRole}"/></param>
    public class ApplicationRoleService(IMapper @mapper,
                                        ILogger<ApplicationRoleService> @logger,
                                        RoleManager<ApplicationRole> @roleManager) : BaseService(@mapper), IApplicationRoleService
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
                ConcurrencyStamp = DateTime.UtcNow.ToBinary().ToString(),
                ImageUri = @viewModel.ImageUri.Trim(),
            };

            IdentityResult @identityResult = await @roleManager.CreateAsync(@applicationRole);

            if (@identityResult.Succeeded)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Id "
                    + @applicationRole.Id
                    + " was added at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteInsertItemLog(@logData);

                return Mapper.Map<ViewApplicationRole>(@applicationRole);
            }
            else
            {
                throw new ServiceException("Management Error");
            }

        }

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        public async Task<ApplicationRole> CheckName(AddApplicationRole @viewModel)
        {
            ApplicationRole @applicationRole = await @roleManager.Roles
                .AsNoTracking()
                .TagWith("CheckName")
                .FirstOrDefaultAsync(x => x.Name == @viewModel.Name.Trim());

            if (@applicationRole is not null)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Name "
                    + @applicationRole.Name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemFoundLog(@logData);

                throw new ServiceException(nameof(@applicationRole)
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
            ApplicationRole @applicationRole = await @roleManager.Roles
                 .AsNoTracking()
                 .AsSplitQuery()
                 .TagWith("CheckName")
                 .FirstOrDefaultAsync(x => x.Name == @viewModel.Name.Trim() && x.Id != @viewModel.Id);

            if (@applicationRole is not null)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Name "
                    + applicationRole.Name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemFoundLog(@logData);

                throw new ServiceException(nameof(@applicationRole)
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
            ICollection<ApplicationRole> @applicationRoles = await @roleManager.Roles
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
                Length = await @roleManager.Roles
                    .TagWith("CountAllApplicationRole")
                    .AsNoTracking()
                    .AsSplitQuery()
                    .CountAsync(),
                Index = @viewModel.Index,
                Size = @viewModel.Size,
                Items = Mapper.Map<IList<ViewApplicationRole>>(await @roleManager.Roles
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
            ApplicationRole @applicationRole = await @roleManager.Roles.
                TagWith("FindApplicationRoleById")
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationRole is null)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Id "
                    + @id
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new ServiceException(nameof(@applicationRole)
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

            IdentityResult @identityResult = await @roleManager.DeleteAsync(@applicationRole);

            if (@identityResult.Succeeded)
            {
                // Log
                string @logData = nameof(@applicationRole)
                    + " with Id "
                    + @applicationRole.Id
                    + " was removed at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteDeleteItemLog(@logData);
            }
            else
            {
                throw new ServiceException("Management Error");
            }
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

            IdentityResult @identityResult = await @roleManager.UpdateAsync(@applicationRole);

            if (@identityResult.Succeeded)
            {

                // Log
                string @logData = nameof(@applicationRole)
                    + " with Id "
                    + @applicationRole.Id
                    + " was modified at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteUpdateItemLog(@logData);

                return Mapper.Map<ViewApplicationRole>(@applicationRole);

            }
            else
            {
                throw new ServiceException("Management Error");
            }
        }
    }
}
