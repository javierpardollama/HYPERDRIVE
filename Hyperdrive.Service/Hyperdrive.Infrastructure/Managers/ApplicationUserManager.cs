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
    /// Represents a <see cref="ApplicationUserManager"/> class. Implements <see cref="IApplicationUserManager"/>
    /// </summary>    
    /// <param name="logger">Injected <see cref="ILogger{ApplicationUserManager}"/></param>
    /// <param name="userManager">Injected <see cref=" UserManager{ApplicationUser}"/></param>
    public class ApplicationUserManager(
        ILogger<ApplicationUserManager> @logger,
        UserManager<ApplicationUser> @userManager) : IApplicationUserManager
    {

        /// <summary>
        /// Finds All Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{CatalogDto}}"/></returns>
        public async Task<ICollection<CatalogDto>> FindAllApplicationUser()
        {
            ICollection<CatalogDto> @applicationUsers = await @userManager.Users
                .TagWith("FindAllApplicationUser")
                .AsNoTracking()
                .AsSplitQuery()
                .Select(x => x.ToCatalog())
                .ToListAsync();

            return @applicationUsers;
        }

        /// <summary>
        /// Finds Paginated Application User
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{ApplicationUserDto}}"/></returns>
        public async Task<PageDto<ApplicationUserDto>> FindPaginatedApplicationUser(int index, int size)
        {
            PageDto<ApplicationUserDto> @page = new()
            {
                Length = await @userManager.Users
                    .TagWith("CountAllApplicationUser")
                    .AsNoTracking()
                    .AsSplitQuery()
                    .CountAsync(),
                Index = @index,
                Size = @size,
                Items = await @userManager.Users
                    .TagWith("FindPaginatedApplicationUser")
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(x => x.ApplicationUserRoles)
                    .ThenInclude(x => x.ApplicationRole)
                    .Skip(@index * @size)
                    .Take(@size)
                    .Select(x => x.ToDto())
                    .ToListAsync()
            };

            return @page;
        }

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> FindApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await @userManager.Users
                .TagWith("FindApplicationUserById")
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(ApplicationUser)
                                  + " with Id "
                                  + @id
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                                           + " with Id "
                                           + @id
                                           + " does not exist");
            }

            return @applicationUser;
        }

        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> FindApplicationUserByEmail(string @email)
        {

            ApplicationUser @applicationUser = await @userManager.Users
                .TagWith("FindApplicationUserById")
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == @email);

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(ApplicationUser)
                                  + " with Email "
                                  + @email
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                                           + " with Email "
                                           + @email
                                           + " does not exist");
            }

            return @applicationUser;
        }

        /// <summary>
        /// Removes Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task RemoveApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@id);

            IdentityResult @identityResult = await @userManager.DeleteAsync(@applicationUser);

            if (!@identityResult.Succeeded) throw new ServiceException("Management Error");

            // Log
            string @logData = nameof(ApplicationUser)
                              + " with Id "
                              + @applicationUser.Id
                              + " was removed at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Updates Application User Roles
        /// </summary>
        /// <param name="roles">Injected <see cref="List{string}"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        public async Task<ApplicationUserDto> UpdateApplicationUserRoles(List<string> @roles, int @id)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@id);

            IdentityResult @identityResult = await @userManager.AddToRolesAsync(applicationUser, roles);

            if (!@identityResult.Succeeded) throw new ServiceException("Management Error");

            // Log
            string @logData = nameof(ApplicationUser)
                              + " with Id"
                              + @id
                              + " was modified at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return applicationUser.ToDto();
        }

        /// <summary>
        /// Checks Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> CheckEmail(string @email, int @id)
        {
            ApplicationUser @applicationUser = await @userManager.Users
                .AsNoTracking()
                .AsSplitQuery()
                .TagWith("CheckEmail")
                .FirstOrDefaultAsync(x => x.Email == @email.Trim() && x.Id != @id);

            if (@applicationUser is not null)
            {
                // Log
                string @logData = nameof(ApplicationUser)
                                  + " with Email "
                                  + applicationUser.Email
                                  + " was already found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                                           + " with Email "
                                           + @email
                                           + " already exists");
            }

            return @applicationUser;
        }

        /// <summary>
        /// Reloads Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUserDto> ReloadApplicationUserById(int @id)
        {
            ApplicationUserDto @applicationUser = await @userManager.Users
                .TagWith("FindApplicationUserById")
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .Include(x => x.ApplicationUserRefreshTokens)
                .Include(x => x.ApplicationUserTokens)
                .Where(x => x.Id == @id)
                .Select(x => x.ToDto())
                .FirstOrDefaultAsync();

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(ApplicationUser)
                                  + " with Id "
                                  + @id
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.LogWarning(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                                           + " with Id "
                                           + @id
                                           + " does not exist");
            }

            return @applicationUser;
        }
    }
}
