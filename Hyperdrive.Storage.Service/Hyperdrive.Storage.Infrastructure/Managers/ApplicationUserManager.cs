using Hyperdrive.Storage.Domain.Entities;
using Hyperdrive.Storage.Domain.Exceptions;
using Hyperdrive.Storage.Domain.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Storage.Infrastructure.Managers;

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
}
