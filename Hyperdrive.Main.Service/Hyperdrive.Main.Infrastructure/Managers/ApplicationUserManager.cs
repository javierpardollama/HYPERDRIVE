using Hyperdrive.Main.Domain.Dtos;
using Hyperdrive.Main.Domain.Entities;
using Hyperdrive.Main.Domain.Exceptions;
using Hyperdrive.Main.Domain.Managers;
using Hyperdrive.Main.Domain.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Infrastructure.Managers;

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
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .OrderByDescending(x => x.CreatedAt)
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
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Where(x => x.Id == @id)
            .FirstOrDefaultAsync();

        if (@applicationUser is null)
        {
            // Log
            string @logData = nameof(ApplicationUser)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(ApplicationUser)
                                       + " does not exist");
        }

        return @applicationUser;
    }

    /// <summary>
    /// Finds All Application User By Ids
    /// </summary>
    /// <param name="ids">Injected <see cref="ICollection{int}"/></param>
    /// <returns>Instance of <see cref="Task{IList{ApplicationUser}}"/></returns>
    public async Task<IList<ApplicationUser>> FindAllApplicationUserByIds(ICollection<int> @ids)
    {
        var @tasks = @ids.Select(@id => FindApplicationUserById(@id));
        var users = await Task.WhenAll(tasks);
        return [.. users];
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
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Where(x => x.Email == @email)
            .FirstOrDefaultAsync();

        if (@applicationUser is null)
        {
            // Log
            string @logData = nameof(ApplicationUser)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(ApplicationUser)
                                       + " does not exist");
        }

        return @applicationUser;
    }

    /// <summary>
    /// Removes Application User
    /// </summary>
    /// <param name="id">Injected <see cref="ApplicationUser"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task RemoveApplicationUser(ApplicationUser @user)
    {
        IdentityResult @identityResult = await @userManager.DeleteAsync(@user);

        if (!@identityResult.Succeeded) throw new ServiceException("Management Error");

        // Log
        string @logData = nameof(ApplicationUser)
                          + " was removed at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);
    }

    /// <summary>
    /// Updates Application User Roles
    /// </summary>
    /// <param name="roles">Injected <see cref="ICollection{string}"/></param>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
    public async Task<ApplicationUserDto> UpdateApplicationUserRoles(ICollection<string> @roles, ApplicationUser @user)
    {
        await RemoveApplicationUserRoles(@user);

        await AddApplicationUserRoles(roles, @user);

        // Log
        string @logData = nameof(ApplicationUser)
                          + " was modified at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @user.ToDto();
    }

    /// Adds Application Roles to Application User
    /// </summary>
    /// <param name="roles">Injected <see cref="ICollection{string}"/></param>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> AddApplicationUserRoles(ICollection<string> @roles, ApplicationUser @user)
    {
        IdentityResult @identityResult = await @userManager.AddToRolesAsync(@user, @roles);

        if (!@identityResult.Succeeded) throw new ServiceException("Management Error");

        // Log
        string @logData = nameof(ApplicationUser)
                          + " added its "
                          + nameof(ApplicationRole)
                          + " at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @identityResult.Succeeded;
    }

    /// <summary>
    /// Removes Application Roles from Application User
    /// </summary>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> RemoveApplicationUserRoles(ApplicationUser user)
    {
        var @roles = await userManager.GetRolesAsync(user);

        IdentityResult @identityResult = await @userManager.RemoveFromRolesAsync(user, roles);

        if (!@identityResult.Succeeded) throw new ServiceException("Management Error");

        // Log
        string @logData = nameof(ApplicationUser)
                          + " removed its "
                          + nameof(ApplicationRole)
                          + " at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @identityResult.Succeeded;
    }

    /// <summary>
    /// Checks Email
    /// </summary>
    /// <param name="email">Injected <see cref="string"/></param>
    /// <param name="id">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> CheckEmail(string @email, int @id)
    {
        var @found = await @userManager.Users
            .AsNoTracking()
            .AsSplitQuery()
            .TagWith("CheckEmail")
            .Where(x => x.Email == @email.Trim() && x.Id != @id)
            .AnyAsync();

        if (@found)
        {
            // Log
            string @logData = nameof(ApplicationUser)
                              + " was already found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(ApplicationUser)
                                       + " already exists");
        }

        return @found;
    }

    /// <summary>
    /// Checks Email
    /// </summary>
    /// <param name="email">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
    public async Task<bool> CheckEmail(string @email)
    {
        var @found = await @userManager.Users
            .AsNoTracking()
            .AsSplitQuery()
            .TagWith("CheckEmail")
            .Where(x => x.Email == @email.Trim())
            .AnyAsync();

        if (@found)
        {
            // Log
            string @logData = nameof(ApplicationUser)
                              + " was already found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(ApplicationUser)
                                       + " already exists");
        }

        return @found;
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
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Include(x => x.RefreshTokens)
            .Include(x => x.Tokens)
            .Where(x => x.Id == @id)
            .Select(x => x.ToDto())
            .FirstOrDefaultAsync();

        if (@applicationUser is null)
        {
            // Log
            string @logData = nameof(ApplicationUser)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(ApplicationUser)
                                       + " does not exist");
        }

        return @applicationUser;
    }
}
