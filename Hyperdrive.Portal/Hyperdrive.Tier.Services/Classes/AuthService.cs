using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Auth;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="AuthService"/> interface. Inherits <see cref="BaseService"/>. Implemenets <see cref="IAuthService"/>
    /// </summary>
    public class AuthService : BaseService, IAuthService
    {
        private readonly SignInManager<ApplicationUser> SignInManager;

        private readonly UserManager<ApplicationUser> UserManager;

        private readonly ITokenService TokenService;

        /// <summary>
        /// Initializes a new Instance of <see cref="AuthService"/>
        /// </summary>
        /// <param name="mapper">Injected <see cref="IMapper"/></param>
        /// <param name="logger">Injected <see cref="ILogger{AuthService}"/></param>
        /// <param name="configuration">Injected <see cref="IConfiguration"/></param>
        /// <param name="userManager">Injected <see cref=" UserManager{ApplicationUser}"/></param>
        /// <param name="signInManager">Injected <see cref=" SignInManager{ApplicationUser}"/></param>
        /// <param name="tokenService">Injected <see cref="ITokenService"/></param>
        public AuthService(IMapper @mapper,
                           ILogger<AuthService> @logger,
                           IConfiguration @configuration,
                           UserManager<ApplicationUser> @userManager,
                           SignInManager<ApplicationUser> @signInManager,
                           ITokenService @tokenService) : base(@mapper, @logger, @configuration)
        {
            UserManager = @userManager;
            SignInManager = @signInManager;
            TokenService = @tokenService;
        }

        /// <summary>
        /// Signs In
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthSignIn"/></param>
        /// <returns>Instance of <see cref="ViewApplicationUser"/></returns>
        public async Task<ViewApplicationUser> SignIn(AuthSignIn @viewModel)
        {
            SignInResult signInResult = await SignInManager.PasswordSignInAsync(@viewModel.Email,
                                                                                @viewModel.Password,
                                                                                false,
                                                                                true);

            if (signInResult.Succeeded)
            {
                ApplicationUser @applicationUser = await FindApplicationUserByEmail(@viewModel.Email);

                @applicationUser.ApplicationUserTokens.Add(new ApplicationUserToken
                {
                    Name = Guid.NewGuid().ToString(),
                    LoginProvider = JwtSettings.JwtIssuer,
                    ApplicationUser = @applicationUser,
                    UserId = @applicationUser.Id,
                    Value = TokenService.WriteJwtToken(TokenService.GenerateJwtToken(@applicationUser))
                });

                // Log
                string @logData = @applicationUser.GetType().Name
                    + " with Email "
                    + @applicationUser.Email
                    + " logged in at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteUserAuthenticatedLog(@logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new Exception("Authentication Error");
            }
        }

        /// <summary>
        /// Signs In
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
        /// <returns>Instance of <see cref="ViewApplicationUser"/></returns>
        public async Task<ViewApplicationUser> SignIn(AuthJoinIn @viewModel)
        {
            SignInResult @signInResult = await SignInManager.PasswordSignInAsync(@viewModel.Email,
                                                                                @viewModel.Password,
                                                                                false,
                                                                                true);

            if (@signInResult.Succeeded)
            {
                ApplicationUser @applicationUser = await FindApplicationUserByEmail(@viewModel.Email);

                @applicationUser.ApplicationUserTokens.Add(new ApplicationUserToken
                {
                    Name = Guid.NewGuid().ToString(),
                    LoginProvider = JwtSettings.JwtIssuer,
                    ApplicationUser = @applicationUser,
                    UserId = @applicationUser.Id,
                    Value = TokenService.WriteJwtToken(TokenService.GenerateJwtToken(@applicationUser))
                });

                // Log
                string logData = @applicationUser.GetType().Name
                    + " with Email "
                    + @applicationUser.Email
                    + " logged in at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteUserAuthenticatedLog(logData);

                return Mapper.Map<ViewApplicationUser>(@applicationUser);
            }
            else
            {
                throw new Exception("Authentication Error");
            }
        }

        /// <summary>
        /// Joins In
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AuthJoinIn"/></param>
        /// <returns>Instance of <see cref="ViewApplicationUser"/></returns>
        public async Task<ViewApplicationUser> JoinIn(AuthJoinIn @viewModel)
        {
            await CheckEmail(@viewModel);

            ApplicationUser @applicationUser = new()
            {
                UserName = @viewModel.Email,
                Email = @viewModel.Email,
                ConcurrencyStamp = DateTime.Now.ToBinary().ToString(),
                SecurityStamp = DateTime.Now.ToBinary().ToString(),
                NormalizedEmail = @viewModel.Email,
                NormalizedUserName = @viewModel.Email,
                LastModified = DateTime.Now,
                Deleted = false
            };

            IdentityResult @identityResult = await UserManager.CreateAsync(@applicationUser,
                                                                          @viewModel.Password);

            if (@identityResult.Succeeded)
            {
                return await SignIn(@viewModel);
            }
            else
            {
                throw new Exception("Authentication Error");
            }
        }

        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="ApplicationUser"/></returns>
        public async Task<ApplicationUser> FindApplicationUserByEmail(string @email)
        {
            ApplicationUser @applicationUser = await UserManager.Users
                .TagWith("FindApplicationUserByEmail")
                .AsQueryable()
                .Include(x => x.ApplicationUserTokens)
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == @email);

            if (@applicationUser == null)
            {
                // Log
                string @logData = @applicationUser.GetType().Name
                    + " with Email "
                    + @email
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(@logData);

                throw new Exception(@applicationUser.GetType().Name
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
        /// <returns>Instance of <see cref="ApplicationUser"/></returns>
        public async Task<ApplicationUser> CheckEmail(AuthJoinIn @viewModel)
        {
            ApplicationUser @applicationUser = await UserManager.Users
              .AsNoTracking()
              .TagWith("CheckEmail")
              .FirstOrDefaultAsync(x => x.Email == @viewModel.Email);

            if (@applicationUser != null)
            {
                // Log
                string @logData = @applicationUser.GetType().Name
                    + " with Email "
                    + @viewModel.Email
                      + " was already found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemFoundLog(@logData);

                throw new Exception(@applicationUser.GetType().Name
                    + " with Email "
                    + @viewModel.Email
                    + " already exists");
            }

            return @applicationUser;
        }
    }
}
