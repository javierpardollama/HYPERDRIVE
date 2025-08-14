using AutoMapper;
using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Exceptions.Exceptions;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Hyperdrive.Tier.ViewModels.Classes.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="SecurityService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="ISecurityService"/>
    /// </summary>   
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="mapper">Injected <see cref="IMapper"/></param>
    /// <param name="logger">Injected <see cref="ILogger{SecurityService}"/></param>
    /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
    /// <param name="userManager">Injected <see cref=" UserManager{ApplicationUser}"/></param>
    /// <param name="tokenService">Injected <see cref="ITokenService"/></param>
    /// <param name="tokenRefreshService">Injected <see cref="IRefreshTokenService"/></param>
    public class SecurityService(IApplicationContext @context, IMapper @mapper,
                       ILogger<SecurityService> @logger,
                       IOptions<JwtSettings> @jwtSettings,
                       UserManager<ApplicationUser> @userManager,
                       ITokenService @tokenService,
                       IRefreshTokenService @tokenRefreshService) : BaseService(@context, @mapper, @jwtSettings), ISecurityService
    {

        /// <summary>
        /// Changes Password
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPasswordChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> ChangePassword(SecurityPasswordChange @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@viewModel.ApplicationUserId);

            IdentityResult @identityResult = await @userManager.ChangePasswordAsync(@applicationUser, @viewModel.CurrentPassword, @viewModel.NewPassword);

            if (@identityResult.Succeeded)
            {
                await tokenRefreshService.AddApplicationUserRefreshToken(viewModel.ApplicationUserId);
            
                await tokenService.AddApplicationUserToken(viewModel.ApplicationUserId);  
                
                @applicationUser = await ReloadApplicationUserById(@viewModel.ApplicationUserId);

                // Log
                string @logData = nameof(ApplicationUser)
                    + " with Id "
                    + @applicationUser.Id
                    + " restored its Password at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WritePasswordRestoredLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new UnauthorizedAccessException("Security Error");
            }
        }

        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> FindApplicationUserByEmail(string @email)
        {
            ApplicationUser @applicationUser = await @userManager.Users
                .TagWith("FindApplicationUserByEmail")
                .FirstOrDefaultAsync(x => x.Email == @email.Trim());

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(ApplicationUser)
                    + " with Email "
                    + @email
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new UnauthorizedAccessException(nameof(@applicationUser)
                    + " with Email "
                    + @email
                    + " does not exist");
            }

            return @applicationUser;
        }

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> FindApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await @userManager.Users
                .TagWith("FindApplicationUserByEmail")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(ApplicationUser)
                    + " with Id "
                    + @id
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                    + " with Id "
                    + @id
                    + " does not exist");
            }

            return @applicationUser;
        }

        /// <summary>
        /// Resets Password
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPasswordReset"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> ResetPassword(SecurityPasswordReset @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserByEmail(@viewModel.Email);

            IdentityResult @identityResult = await @userManager.ResetPasswordAsync(@applicationUser, await @userManager.GeneratePasswordResetTokenAsync(@applicationUser), @viewModel.NewPassword.Trim());

            if (@identityResult.Succeeded)
            {
                await tokenRefreshService.AddApplicationUserRefreshToken(@applicationUser.Id);
            
                await tokenService.AddApplicationUserToken(@applicationUser.Id);  
                
                @applicationUser = await ReloadApplicationUserById(@applicationUser.Id);
                
                // Log
                string @logData = nameof(ApplicationUser)
                    + " with Id "
                    + @applicationUser.Id
                    + " restored its Password at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WritePasswordRestoredLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new UnauthorizedAccessException("Security Error");
            }
        }

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityEmailChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> ChangeEmail(SecurityEmailChange @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@viewModel.ApplicationUserId);

            IdentityResult @identityResult = await @userManager.ChangeEmailAsync(@applicationUser, @viewModel.NewEmail.Trim(), await @userManager.GenerateChangeEmailTokenAsync(@applicationUser, @viewModel.NewEmail.Trim()));

            if (@identityResult.Succeeded)
            {
                await tokenRefreshService.AddApplicationUserRefreshToken(viewModel.ApplicationUserId);
            
                await tokenService.AddApplicationUserToken(viewModel.ApplicationUserId);  
                
                @applicationUser = await ReloadApplicationUserById(@viewModel.ApplicationUserId);

                // Log
                string @logData = nameof(ApplicationUser)
                    + " with Id "
                    + @applicationUser.Email
                    + " restored its Email at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteEmailRestoredLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new UnauthorizedAccessException("Security Error");
            }
        }

        /// <summary>
        /// Changes Phone Number
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPhoneNumberChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> ChangePhoneNumber(SecurityPhoneNumberChange @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@viewModel.ApplicationUserId);

            IdentityResult @identityResult = await @userManager.ChangePhoneNumberAsync(@applicationUser, @viewModel.NewPhoneNumber.Trim(), await @userManager.GenerateChangePhoneNumberTokenAsync(@applicationUser, @viewModel.NewPhoneNumber.Trim()));

            if (@identityResult.Succeeded)
            {
                await tokenRefreshService.AddApplicationUserRefreshToken(viewModel.ApplicationUserId);
            
                await tokenService.AddApplicationUserToken(viewModel.ApplicationUserId);  
                
                @applicationUser = await ReloadApplicationUserById(@viewModel.ApplicationUserId);
                
                // Log
                string @logData = nameof(ApplicationUser)
                    + " with Id "
                    + @applicationUser.Id
                    + " restored its Phone Number at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WritePhoneNumberRestoredLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new UnauthorizedAccessException("Security Error");
            }
        }

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityNameChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> ChangeName(SecurityNameChange @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@viewModel.ApplicationUserId);

            @applicationUser.FirstName = viewModel.NewFirstName;
            @applicationUser.LastName = viewModel.NewLastName;

            IdentityResult @identityResult = await @userManager.UpdateAsync(@applicationUser);

            if (@identityResult.Succeeded)
            {
                await tokenRefreshService.AddApplicationUserRefreshToken(viewModel.ApplicationUserId);
            
                await tokenService.AddApplicationUserToken(viewModel.ApplicationUserId);   
            
                @applicationUser = await FindApplicationUserById(@viewModel.ApplicationUserId);
                
                // Log
                string @logData = nameof(ApplicationUser)
                    + " with Id "
                    + @applicationUser.Id
                    + " was modified at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteUpdateItemLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new UnauthorizedAccessException("Security Error");
            }
        }

        /// <summary>
        /// Refreshes Tokens
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityRefreshTokenReset"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> RefreshTokens(SecurityRefreshTokenReset viewModel)
        {
            await tokenRefreshService.IsRevoked(viewModel);

            await tokenRefreshService.Revoke(viewModel);

            await tokenRefreshService.AddApplicationUserRefreshToken(viewModel.ApplicationUserId);
            
            await tokenService.AddApplicationUserToken(viewModel.ApplicationUserId);  
                
            ApplicationUser @applicationUser = await ReloadApplicationUserById(@viewModel.ApplicationUserId);

            // Log
            string @logData = nameof(ApplicationUser)
                + " with Id "
                + @applicationUser.Id
                + " restored its Tokens at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.WriteTokensRefreshedLog(@logData);

            return Mapper.Map<ViewApplicationUser>(@applicationUser);
        }
        
        /// <summary>
        /// Reloads Application User By Id
        /// </summary>
        /// <param name="userid">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> ReloadApplicationUserById(int @userid)
        {
            ApplicationUser @applicationUser = await @userManager.Users
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.ApplicationUserRefreshTokens)
                .Include(x=> x.ApplicationUserTokens)
                .Include(x=>x.ApplicationUserRoles)
                .ThenInclude(x=> x.ApplicationRole)
                .TagWith("ReloadApplicationUserById")
                .FirstOrDefaultAsync(x => x.Id == @userid);

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(@applicationUser)
                                  + " with Id "
                                  + @userid
                                  + " was not found at "
                                  + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new ServiceException(nameof(ApplicationUser)
                                           + " with Id "
                                           + @userid
                                           + " does not exist");
            }

            return @applicationUser;
        }
    }
}
