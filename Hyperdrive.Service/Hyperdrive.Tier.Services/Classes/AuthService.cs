﻿using AutoMapper;
using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Exceptions.Exceptions;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.Settings.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Auth;
using Hyperdrive.Tier.ViewModels.Classes.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="AuthService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IAuthService"/>
    /// </summary>
    /// <param name="context">Injected <see cref="IApplicationContext"/></param>
    /// <param name="mapper">Injected <see cref="IMapper"/></param>
    /// <param name="logger">Injected <see cref="ILogger{AuthService}"/></param>
    /// <param name="jwtSettings">Injected <see cref="IOptions{JwtSettings}"/></param>
    /// <param name="userManager">Injected <see cref=" UserManager{ApplicationUser}"/></param>
    /// <param name="signInManager">Injected <see cref=" SignInManager{ApplicationUser}"/></param>
    /// <param name="tokenService">Injected <see cref="ITokenService"/></param>
    /// <param name="tokenRefreshService">Injected <see cref="IRefreshTokenService"/></param>
    public class AuthService(IApplicationContext @context,
                             IMapper @mapper,
                             ILogger<AuthService> @logger,
                             IOptions<JwtSettings> @jwtSettings,
                             UserManager<ApplicationUser> @userManager,
                             SignInManager<ApplicationUser> @signInManager,
                             ITokenService @tokenService,
                             IRefreshTokenService @tokenRefreshService) : BaseService(@context, @mapper, @jwtSettings), IAuthService
    {

        /// <summary>
        /// Signs In
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthSignIn"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> SignIn(AuthSignIn @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserByEmail(@viewModel.Email);

            SignInResult @signInResult = await @signInManager.PasswordSignInAsync(@applicationUser,
                                                                                @viewModel.Password,
                                                                                false,
                                                                                true);

            if (@signInResult.Succeeded)
            {
                @applicationUser.ApplicationUserTokens.Add(new ApplicationUserToken
                {
                    Name = Guid.NewGuid().ToString(),
                    LoginProvider = JwtSettings.Value.JwtIssuer,
                    ApplicationUser = @applicationUser,
                    UserId = @applicationUser.Id,
                    Value = tokenService.CreateToken(tokenService.GenerateTokenDescriptor(@applicationUser))
                });

                @applicationUser.ApplicationUserRefreshTokens.Add(new ApplicationUserRefreshToken
                {

                    Name = Guid.NewGuid().ToString(),
                    LoginProvider = JwtSettings.Value.JwtIssuer,
                    ApplicationUser = @applicationUser,
                    Value = tokenRefreshService.WriteJwtRefreshToken(),
                    ExpiresAt = tokenRefreshService.GenerateRefreshTokenExpirationDate()
                });

                // Log
                string @logData = nameof(@applicationUser)
                    + " with Email "
                    + @applicationUser.Email
                    + " logged in at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteUserAuthenticatedLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new UnauthorizedAccessException("Authentication Error");
            }
        }

        /// <summary>
        /// Signs In
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> SignIn(AuthJoinIn @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserByEmail(@viewModel.Email);

            SignInResult @signInResult = await @signInManager.PasswordSignInAsync(@applicationUser,
                                                                                @viewModel.Password,
                                                                                false,
                                                                                true);

            if (@signInResult.Succeeded)
            {
                @applicationUser.ApplicationUserTokens.Add(new ApplicationUserToken
                {
                    Name = Guid.NewGuid().ToString(),
                    LoginProvider = JwtSettings.Value.JwtIssuer,
                    ApplicationUser = @applicationUser,
                    UserId = @applicationUser.Id,
                    Value = tokenService.CreateToken(tokenService.GenerateTokenDescriptor(@applicationUser))
                });

                @applicationUser.ApplicationUserRefreshTokens.Add(new ApplicationUserRefreshToken
                {

                    Name = Guid.NewGuid().ToString(),
                    LoginProvider = JwtSettings.Value.JwtIssuer,
                    ApplicationUser = @applicationUser,
                    Value = tokenRefreshService.WriteJwtRefreshToken(),
                    ExpiresAt = tokenRefreshService.GenerateRefreshTokenExpirationDate()
                });

                // Log
                string @logData = nameof(@applicationUser)
                    + " with Email "
                    + @applicationUser.Email
                    + " logged in at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteUserAuthenticatedLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new UnauthorizedAccessException("Authentication Error");
            }
        }

        /// <summary>
        /// Joins In
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> JoinIn(AuthJoinIn @viewModel)
        {
            await CheckEmail(@viewModel);

            ApplicationUser @applicationUser = new()
            {
                UserName = @viewModel.Email.Trim().Split('@').First(),
                Email = @viewModel.Email.Trim(),
                ConcurrencyStamp = DateTime.UtcNow.ToBinary().ToString(),
                SecurityStamp = DateTime.UtcNow.ToBinary().ToString(),
                NormalizedEmail = @viewModel.Email.Trim().ToUpper(),
                NormalizedUserName = @viewModel.Email.Trim().Split('@').First().ToUpper()
            };

            IdentityResult @identityResult = await @userManager.CreateAsync(@applicationUser,
                                                                          @viewModel.Password);

            if (@identityResult.Succeeded)
            {
                return await SignIn(@viewModel);
            }
            else
            {
                throw new UnauthorizedAccessException("Authentication Error");
            }
        }

        /// <summary>
        /// Signs Out
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthSignOut"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task SignOut(AuthSignOut @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserByEmail(@viewModel.Email);

            await @signInManager.SignOutAsync();

            // Log
            string @logData = nameof(@applicationUser)
                + " with Email "
                + @applicationUser.Email
                + " logged out at "
                + DateTime.UtcNow.ToShortTimeString();

            @logger.WriteUserUnauthenticatedLog(@logData);
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
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == @email.Trim());

            if (@applicationUser is null)
            {
                // Log
                string @logData = nameof(@applicationUser)
                    + " with Email "
                    + @email
                    + " was not found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemNotFoundLog(@logData);

                throw new ServiceException(nameof(@applicationUser)
                    + " with Email "
                    + @email
                    + " does not exist");
            }

            return @applicationUser;
        }

        /// <summary>
        /// Checks Email
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> CheckEmail(AuthJoinIn @viewModel)
        {
            ApplicationUser @applicationUser = await @userManager.Users
              .AsNoTracking()
              .AsSplitQuery()
              .TagWith("CheckEmail")
              .FirstOrDefaultAsync(x => x.Email == @viewModel.Email.Trim());

            if (@applicationUser is not null)
            {
                // Log
                string @logData = nameof(@applicationUser)
                    + " with Email "
                    + @viewModel.Email
                      + " was already found at "
                    + DateTime.UtcNow.ToShortTimeString();

                @logger.WriteGetItemFoundLog(@logData);

                throw new ServiceException(nameof(@applicationUser)
                    + " with Email "
                    + @viewModel.Email
                    + " already exists");
            }

            return @applicationUser;
        }
    }
}
