using System;
using System.Linq;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Hyperdrive.Infrastructure.Managers
{
    /// <summary>
    /// Represents a <see cref="AuthManager"/> class. Implements <see cref="IAuthManager"/>
    /// </summary>
    /// <param name="logger">Injected <see cref="ILogger{AuthManager}"/></param>
    /// <param name="userManager">Injected <see cref="UserManager{ApplicationUser}"/></param>
    /// <param name="signInManager">Injected <see cref="SignInManager{ApplicationUser}"/></param>
    public class AuthManager(
        ILogger<AuthManager> @logger,
        UserManager<ApplicationUser> @userManager,
        SignInManager<ApplicationUser> @signInManager) : IAuthManager
    {

        /// <summary>
        /// Signs In
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="password">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public async Task<bool> SignIn(ApplicationUser @user, string email, string password)
        {
            SignInResult @signInResult = await @signInManager.PasswordSignInAsync(@user,
                @password,
                false,
                true);

            if (!@signInResult.Succeeded) throw new UnauthorizedAccessException("Authentication Error");
           
            // Log
            string @logData = nameof(ApplicationUser)                            
                              + " logged in at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @signInResult.Succeeded;
        }
        
        /// <summary>
        /// Signs Out
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public async Task SignOut(string @email)
        {
            await @signInManager.SignOutAsync();
           
            // Log
            string @logData = nameof(ApplicationUser)                             
                              + " logged out at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);
        }

        /// <summary>
        /// Joins In
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="password">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<bool> JoinIn(string @email, string @password)
        {
            ApplicationUser @applicationUser = new()
            {
                UserName = @email.Trim().Split('@').First(),
                Email = @email.Trim(),
                ConcurrencyStamp = DateTime.UtcNow.ToBinary().ToString(),
                SecurityStamp = DateTime.UtcNow.ToBinary().ToString(),
                NormalizedEmail = @email.Trim().ToUpper(),
                NormalizedUserName = @email.Trim().Split('@').First().ToUpper()
            };

            IdentityResult @identityResult = await @userManager.CreateAsync(@applicationUser, @password);

            if (!@identityResult.Succeeded) throw new UnauthorizedAccessException("Authentication Error");
          
            // Log
            string @logData = nameof(ApplicationUser)                           
                              + " joined in at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogInformation(@logData);

            return @identityResult.Succeeded;
        }
    }

}
