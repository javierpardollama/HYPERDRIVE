using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Infrastructure.Managers
{
    /// <summary>
    /// Represents a <see cref="ApplicationRoleManager"/> class. Implements <see cref="IApplicationRoleManager"/>
    /// </summary>  
    /// <param name="logger">Injected <see cref="ILogger{ApplicationRoleManager}"/></param>
    /// <param name="roleManager">Injected <see cref=" RoleManager{ApplicationRole}"/></param>
    public class ApplicationRoleManager(ILogger<ApplicationRoleManager> @logger,
                                        RoleManager<ApplicationRole> @roleManager) : IApplicationRoleManager
    {

        /// <summary>
        /// Adds Application Role
        /// </summary>
        /// <param name="entity">Injected <see cref="ApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationRole}"/></returns>
        public async Task<ApplicationRoleDto> AddApplicationRole(ApplicationRole @entity)
        {
            await CheckName(@entity.Name);

            IdentityResult @identityResult = await @roleManager.CreateAsync(@entity);

            if (!@identityResult.Succeeded) throw new ServiceException("Management Error");
            
            // Log
            string @logData = nameof(ApplicationRole)
                + " with Id "
                + @entity.Id
                + " was added at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @entity.ToDto();
        }

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public async Task<bool> CheckName(string @name)
        {
            var @found = await @roleManager.Roles
                .AsNoTracking()
                .TagWith("CheckName")
                .Where(x => x.Name == @name.Trim())
                .AnyAsync();

            if (@found)
            {
                // Log
                string @logData = nameof(ApplicationRole)
                    + " with Name "
                    + @name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(ApplicationRole)
                    + " with Name "
                    + @name
                    + " already exists");
            }

            return @found;
        }

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public async Task<bool> CheckName(string @name, int @id)
        {
            var @found = await @roleManager.Roles
                 .AsNoTracking()
                 .AsSplitQuery()
                 .TagWith("CheckName")
                 .Where(x => x.Name == @name.Trim() && x.Id != @id)
                 .AnyAsync();

            if (@found)
            {
                // Log
                string @logData = nameof(ApplicationRole)
                    + " with Name "
                    + @name
                    + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(ApplicationRole)
                    + " with Name "
                    + @name
                    + " already exists");
            }

            return @found;
        }

        /// <summary>
        /// Finds All Application Role
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{CatalogDto}}"/></returns>
        public async Task<ICollection<CatalogDto>> FindAllApplicationRole()
        {
            ICollection<CatalogDto> @applicationRoles = await @roleManager.Roles
                .TagWith("FindAllApplicationRole")
                .AsNoTracking()
                .AsSplitQuery()
                .Select(role => role.ToCatalog())
                .ToListAsync();

            return @applicationRoles;
        }

        /// <summary>
        /// Finds Paginated Application Role
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ViewPage{ApplicationRoleDto}}"/></returns>
        public async Task<PageDto<ApplicationRoleDto>> FindPaginatedApplicationRole(int @index, int @size)
        {
            PageDto<ApplicationRoleDto> @page = new()
            {
                Length = await @roleManager.Roles
                    .TagWith("CountAllApplicationRole")
                    .AsNoTracking()
                    .AsSplitQuery()
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = await @roleManager.Roles
               .TagWith("FindPaginatedApplicationRole")
               .AsNoTracking()
               .AsSplitQuery()
               .Select(role => role.ToDto())
               .Skip(@index * @size)
               .Take(@size)
               .ToListAsync()
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
                string @logData = nameof(ApplicationRole)
                    + " with Id "
                    + @id
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(ApplicationRole)
                    + " with Id "
                    + id
                    + " does not exist");
            }

            return @applicationRole;
        }

        /// <summary>
        /// Removes Application Role
        /// </summary>
        /// <param name="entity">Injected <see cref="ApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task RemoveApplicationRole(ApplicationRole @entity)
        {           
            IdentityResult @identityResult = await @roleManager.DeleteAsync(@entity);

            if (!@identityResult.Succeeded) throw new ServiceException("Management Error");
           
            // Log
            string @logData = nameof(ApplicationRole)
                + " with Id "
                + @entity.Id
                + " was removed at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Updates Application Role
        /// </summary>
        /// <param name="entity">Injected <see cref="ApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRoleDto}"/></returns>
        public async Task<ApplicationRoleDto> UpdateApplicationRole(ApplicationRole @entity)
        {
            await CheckName(@entity.Name, @entity.Id);

            @entity = await FindApplicationRoleById(@entity.Id);

            @entity.Name = @entity.Name?.Trim();
            @entity.NormalizedName = @entity.Name?.Trim().ToUpper();
            @entity.ImageUri = @entity.ImageUri.Trim();

            IdentityResult @identityResult = await @roleManager.UpdateAsync(@entity);

            if (!@identityResult.Succeeded) throw new ServiceException("Management Error");
            
            // Log
            string @logData = nameof(ApplicationRole)
                + " with Id "
                + @entity.Id
                + " was modified at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @entity.ToDto();
        }
    }
}
