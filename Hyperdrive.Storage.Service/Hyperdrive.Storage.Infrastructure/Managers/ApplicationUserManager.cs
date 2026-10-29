using Hyperdrive.Storage.Domain.Entities;
using Hyperdrive.Storage.Domain.Exceptions;
using Hyperdrive.Storage.Domain.Managers;
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
}
