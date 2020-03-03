using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Services.Classes
{
    public class SecurityService : BaseService, ISecurityService
    {
        private readonly UserManager<ApplicationUser> UserManager;

        private readonly ITokenService TokenService;

        public SecurityService(IMapper mapper,
                           ILogger<SecurityService> logger,
                           UserManager<ApplicationUser> userManager,
                           ITokenService tokenService) : base(mapper, logger)
        {
            UserManager = userManager;
            TokenService = tokenService;
        }

        public async Task<ViewApplicationUser> ChangePassword(SecurityPasswordChange viewModel)
        {
            ApplicationUser applicationUser = await FindApplicationUserByEmail(viewModel.ApplicationUser.Email);

            IdentityResult identityResult = await UserManager.ChangePasswordAsync(applicationUser, viewModel.CurrentPassword, viewModel.NewPassword);

            if (identityResult.Succeeded)
            {
                applicationUser.ApplicationUserTokens.Add(new ApplicationUserToken
                {
                    ApplicationUser = applicationUser,
                    UserId = applicationUser.Id,
                    Value = TokenService.WriteJwtToken(TokenService.GenerateJwtToken(applicationUser))
                });

                // Log
                string logData = applicationUser.GetType().Name
                    + " with Email "
                    + applicationUser.Email
                    + " restored its Password at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WritePasswordRestoredLog(logData);

                return Mapper.Map<ViewApplicationUser>(applicationUser);
            }
            else
            {
                throw new Exception("Security Error");
            }
        }

        public async Task<ApplicationUser> FindApplicationUserByEmail(string email)
        {
            ApplicationUser applicationUser = await UserManager.Users
                .TagWith("FindApplicationUserByEmail")
                .AsQueryable()
                .Include(x => x.ApplicationUserTokens)
                .Include(x => x.ApplicationUserRoles)
                .ThenInclude(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (applicationUser == null)
            {
                // Log
                string logData = applicationUser.GetType().Name
                    + " with Email "
                    + email
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(logData);

                throw new Exception(applicationUser.GetType().Name
                    + " with Email "
                    + email
                    + " does not exist");
            }

            return applicationUser;
        }

        public async Task<ViewApplicationUser> ResetPassword(SecurityPasswordReset viewModel)
        {
            ApplicationUser applicationUser = await FindApplicationUserByEmail(viewModel.Email);

            IdentityResult identityResult = await UserManager.ResetPasswordAsync(applicationUser, await UserManager.GeneratePasswordResetTokenAsync(applicationUser), viewModel.NewPassword);

            if (identityResult.Succeeded)
            {
                applicationUser.ApplicationUserTokens.Add(new ApplicationUserToken
                {
                    ApplicationUser = applicationUser,
                    UserId = applicationUser.Id,
                    Value = TokenService.WriteJwtToken(TokenService.GenerateJwtToken(applicationUser))
                });

                // Log
                string logData = applicationUser.GetType().Name
                    + " with Email "
                    + applicationUser.Email
                    + " restored its Password at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WritePasswordRestoredLog(logData);

                return Mapper.Map<ViewApplicationUser>(applicationUser);
            }
            else
            {
                throw new Exception("Security Error");
            }
        }

        public async Task<ViewApplicationUser> ChangeEmail(SecurityEmailChange viewModel)
        {
            ApplicationUser applicationUser = await FindApplicationUserByEmail(viewModel.ApplicationUser.Email);

            IdentityResult identityResult = await UserManager.ChangeEmailAsync(applicationUser, viewModel.NewEmail, await UserManager.GenerateChangeEmailTokenAsync(applicationUser, viewModel.NewEmail));

            if (identityResult.Succeeded)
            {
                applicationUser.ApplicationUserTokens.Add(new ApplicationUserToken
                {
                    ApplicationUser = applicationUser,
                    UserId = applicationUser.Id,
                    Value = TokenService.WriteJwtToken(TokenService.GenerateJwtToken(applicationUser))
                });

                // Log
                string logData = applicationUser.GetType().Name
                    + " with Email "
                    + applicationUser.Email
                    + " restored its Email at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteEmailRestoredLog(logData);

                return Mapper.Map<ViewApplicationUser>(applicationUser);
            }
            else
            {
                throw new Exception("Security Error");
            }
        }
    }
}
