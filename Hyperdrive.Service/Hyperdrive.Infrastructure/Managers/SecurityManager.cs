using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="SecurityManager"/> class. Implements <see cref="ISecurityManager"/>
/// </summary>   
/// <param name="userManager">Injected <see cref="UserManager{ApplicationUser}"/></param>
/// <param name="logger">Injected <see cref="ILogger{SecurityManager}"/></param>
public class SecurityManager(UserManager<ApplicationUser> @userManager,
                   ILogger<SecurityManager> @logger) : ISecurityManager
{

    /// <summary>
    /// Changes Password
    /// </summary>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <param name="currentPassword">Injected <see cref="string"/></param>
    /// <param name="newPassword">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> ChangePassword(ApplicationUser @user, string @currentPassword, string @newPassword)
    {
        IdentityResult @identityResult = await @userManager.ChangePasswordAsync(@user, @currentPassword, @newPassword);

        if (!@identityResult.Succeeded)  throw new UnauthorizedAccessException("Security Error");
        
        // Log
        string @logData = nameof(ApplicationUser)             
            + " restored its Password at "
            + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

          return @identityResult.Succeeded;
    }
    
    /// <summary>
    /// Resets Password
    /// </summary>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <param name="newPassword">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> ResetPassword(ApplicationUser @user, string @newPassword)
    {
        IdentityResult @identityResult = await @userManager.ResetPasswordAsync(@user, await @userManager.GeneratePasswordResetTokenAsync(@user), @newPassword.Trim());

        if (!@identityResult.Succeeded) throw new UnauthorizedAccessException("Security Error");
       
        // Log
        string @logData = nameof(ApplicationUser)              
            + " restored its Password at "
            + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @identityResult.Succeeded;
    }

    /// <summary>
    /// Changes Email
    /// </summary>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <param name="email">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> ChangeEmail(ApplicationUser @user, string @email)
    {
        IdentityResult @identityResult = await @userManager.ChangeEmailAsync(@user, @email.Trim(), await @userManager.GenerateChangeEmailTokenAsync(@user, @email.Trim()));

        if (!@identityResult.Succeeded) throw new UnauthorizedAccessException("Security Error");
        
        // Log
        string @logData = nameof(ApplicationUser)               
            + " restored its Email at "
            + DateTime.UtcNow.ToShortTimeString();
        
        @logger.LogInformation(@logData);

         return @identityResult.Succeeded;
    }

    /// <summary>
    /// Changes Phone Number
    /// </summary>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <param name="phoneNumber">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> ChangePhoneNumber(ApplicationUser @user, string @phoneNumber)
    {
        IdentityResult @identityResult = await @userManager.ChangePhoneNumberAsync(@user, @phoneNumber.Trim(), await @userManager.GenerateChangePhoneNumberTokenAsync(@user, @phoneNumber.Trim()));

        if (!@identityResult.Succeeded) throw new UnauthorizedAccessException("Security Error");
            
        // Log
        string @logData = nameof(ApplicationUser)              
            + " restored its Phone Number at "
            + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @identityResult.Succeeded;
    }

    /// <summary>
    /// Changes Name
    /// </summary>
    /// <param name="user">Injected <see cref="ApplicationUser"/></param>
    /// <param name="firstName">Injected <see cref="string"/></param>
    /// <param name="lastName">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> ChangeName(ApplicationUser @user, string @firstName, string @lastName)
    {
        @user.FirstName = @firstName;
        @user.LastName = @lastName;

        IdentityResult @identityResult = await @userManager.UpdateAsync(@user);

        if (!@identityResult.Succeeded) throw new UnauthorizedAccessException("Security Error");
            
        // Log
        string @logData = nameof(ApplicationUser)              
            + " was modified at "
            + DateTime.UtcNow.ToShortTimeString();
        
        @logger.LogInformation(@logData);

        return @identityResult.Succeeded;
    }
}
