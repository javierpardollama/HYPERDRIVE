using Hyperdrive.Storage.Domain.Dtos;
using Hyperdrive.Storage.Domain.Entities;
using Hyperdrive.Storage.Domain.Exceptions;
using Hyperdrive.Storage.Domain.Managers;
using Hyperdrive.Storage.Domain.Profiles;
using Hyperdrive.Storage.Infrastructure.Contexts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Storage.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="ApplicationUserManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IApplicationUserManager"/>
/// </summary>    
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger{ApplicationUserManager}"/></param>
public class ApplicationUserManager(IApplicationContext @context,
    ILogger<ApplicationUserManager> @logger) :  BaseManager(context), IApplicationUserManager
{
    /// <summary>
    /// Finds Application User By Id
    /// </summary>
    /// <param name="id">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
    public async Task<ApplicationUser> FindApplicationUserById(int @id)
    {
        ApplicationUser @applicationUser = await Context.Users
            .TagWith("FindApplicationUserById")
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
    ///     Removes ApplicationUser By Id
    /// </summary>
    /// <param name="id">Injected <see cref="int" /></param>
    /// <returns>Instance of <see cref="Task" /></returns>
    public async Task RemoveApplicationUserById(int @id)
    {
        try
        {
            ApplicationUser @user = await FindApplicationUserById(@id);

            Context.Users.Remove(@user);

            await Context.SaveChangesAsync();

            // Log
            var logData = nameof(ApplicationUser)
                          + " with Id "
                          + @user.Id
                          + " was removed at "
                          + DateTime.Now.ToShortTimeString();

            logger.LogInformation(logData);
        }
        catch (DbUpdateConcurrencyException)
        {
            await FindApplicationUserById(@id);
        }
    }

    /// <summary>
    ///     Updates ApplicationUser
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationUser" /></param>
    /// <returns>Instance of <see cref="Task{ApplicationUser}" /></returns>
    public async Task<ApplicationUser> UpdateApplicationUser(ApplicationUser @entity)
    {
        ApplicationUser @user = await FindApplicationUserById(@entity.Id);
        @user.FirstName = @entity.FirstName.Trim();
        @user.LastName = @entity.LastName.Trim();
       
        Context.Users.Update(@user);

        await Context.SaveChangesAsync();

        // Log
        var logData = nameof(ApplicationUser)
                      + " with Id "
                      + user.Id
                      + " was modified at "
                      + DateTime.Now.ToShortTimeString();

        logger.LogInformation(logData);

        return @user;
    }

    /// <summary>
    ///     Adds ApplicationUser
    /// </summary>
    /// <param name="entity">Injected <see cref="ApplicationUser" /></param>
    /// <returns>Instance of <see cref="Task{ApplicationUser}" /></returns>
    public async Task<ApplicationUser> AddApplicationUser(ApplicationUser @entity)
    {
        ApplicationUser @user = new()
        {
            Id = entity.Id,
            FirstName =  entity.FirstName,
            LastName =  entity.LastName,
        };
     
        await Context.Users.AddAsync(@user);

        await Context.SaveChangesAsync();

        // Log
        var logData = nameof(ApplicationUser)
                      + " with Id "
                      + @user.Id
                      + " was added at "
                      + DateTime.Now.ToShortTimeString();

        logger.LogInformation(logData);

        return @user;
    }
    
    /// <summary>
    ///     Reloads ApplicationUser By Id
    /// </summary>
    /// <param name="id">Injected <see cref="int" /></param>
    /// <returns>Instance of <see cref="Task{CatalogDto}" /></returns>
    public async Task<CatalogDto> ReloadApplicationUserById(int @id)
    {
        CatalogDto @dto = await Context.Users
            .TagWith("ReloadFlagById")
            .AsQueryable()
            .AsSplitQuery()
            .AsNoTracking()
            .Where(x => x.Id == @id)
            .Select(x => x.ToCatalog())
            .FirstOrDefaultAsync();


        if (@dto is null)
        {
            // Log
            var logData = nameof(ApplicationUser)
                          + " with Id "
                          + @id
                          + " was not found at "
                          + DateTime.Now.ToShortTimeString();

            logger.LogError(logData);

            throw new ServiceException(nameof(ApplicationUser)
                                       + " with Id "
                                       + @id
                                       + " does not exist");
        }

        return @dto;
    }
}
