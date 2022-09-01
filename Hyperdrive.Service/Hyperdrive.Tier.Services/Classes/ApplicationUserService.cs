using AutoMapper;

using Hyperdrive.Tier.Contexts.Interfaces;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Logging.Classes;
using Hyperdrive.Tier.Services.Interfaces;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Classes
{
    /// <summary>
    /// Represents a <see cref="ApplicationUserService"/> class. Inherits <see cref="BaseService"/>. Implements <see cref="IApplicationUserService"/>
    /// </summary>
    public class ApplicationUserService : BaseService, IApplicationUserService
    {

        /// <summary>
        /// Initializes a new Instance of <see cref="ApplicationUserService"/>
        /// </summary>
        /// <param name="mapper">Injected <see cref="IMapper"/></param>
        /// <param name="context">Injected <see cref="IApplicationContext"/></param>
        /// <param name="logger">Injected <see cref="ILogger{ApplicationUserService}"/></param>
        public ApplicationUserService(IMapper @mapper,
                                      IApplicationContext @context,
                                      ILogger<ApplicationUserService> @logger) : base(@context, @mapper, @logger)
        {
        }

        /// <summary>
        /// Finds All Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{ViewApplicationUser}}"/></returns>
        public async Task<ICollection<ViewApplicationUser>> FindAllApplicationUser()
        {
            ICollection<ApplicationUser> @applicationUsers = await Context.ApplicationUser
               .TagWith("FindAllApplicationUser")
               .AsQueryable()
               .AsNoTracking()
               .Include(x => x.ApplicationUserTokens)
               .Include(x => x.ApplicationUserRoles)
               .ThenInclude(x => x.ApplicationRole)
               .ToListAsync();

            return Mapper.Map<ICollection<ViewApplicationUser>>(@applicationUsers);
        }

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> FindApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await Context.ApplicationUser
               .TagWith("FindApplicationUserById")
               .AsQueryable()
               .Include(x => x.ApplicationUserTokens)
               .Include(x => x.ApplicationUserRoles)
               .ThenInclude(x => x.ApplicationRole)
               .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationUser == null)
            {
                // Log
                string @logData = @applicationUser.GetType().Name
                    + " with Email "
                    + @applicationUser.Email
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(@logData);

                throw new Exception(@applicationUser.GetType().Name
                    + " with Email "
                    + applicationUser.Email
                    + " does not exist");
            }

            return @applicationUser;
        }

        /// <summary>
        /// Removes Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task RemoveApplicationUserById(int @id)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@id);

            Context.ApplicationUser.Remove(@applicationUser);

            await Context.SaveChangesAsync();

            // Log
            string @logData = @applicationUser.GetType().Name
                + " with Id "
                + @applicationUser.Id
                + " was removed at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteDeleteItemLog(@logData);
        }

        /// <summary>
        /// Updates Application User
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        public async Task<ViewApplicationUser> UpdateApplicationUser(UpdateApplicationUser @viewModel)
        {
            ApplicationUser @applicationUser = await FindApplicationUserById(@viewModel.Id);

            @applicationUser.ApplicationUserRoles = new List<ApplicationUserRole>();

            Context.ApplicationUser.Update(@applicationUser);

            UpdateApplicationUserRole(@viewModel, @applicationUser);

            await Context.SaveChangesAsync();

            // Log
            string @logData = @applicationUser.GetType().Name
                + " with Id"
                + @applicationUser.Id
                + " was modified at "
                + DateTime.Now.ToShortTimeString();

            Logger.WriteUpdateItemLog(@logData);

            return Mapper.Map<ViewApplicationUser>(@applicationUser); ;
        }

        /// <summary>
        /// Updates Application User Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationUser"/></param>
        /// <param name="entity">Injected <see cref="ApplicationUser"/></param>
        public void UpdateApplicationUserRole(UpdateApplicationUser @viewModel, ApplicationUser @applicationUser)
        {
            @viewModel.ApplicationRolesId.AsQueryable().ToList().ForEach(async x =>
            {
                ApplicationRole @applicationRole = await FindApplicationRoleById(x);

                ApplicationUserRole @applicationUserRole = new()
                {
                    UserId = @applicationUser.Id,
                    RoleId = @applicationUser.Id,
                };

                @applicationUser.ApplicationUserRoles.Add(@applicationUserRole);
            });
        }

        /// <summary>
        /// Finds Application Role By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        public async Task<ApplicationRole> FindApplicationRoleById(int @id)
        {
            ApplicationRole @applicationRole = await Context.ApplicationRole
                .TagWith("FindApplicationRoleById")
                .FirstOrDefaultAsync(x => x.Id == @id);

            if (@applicationRole == null)
            {
                // Log
                string @logData = @applicationRole.GetType().Name
                    + " with Id "
                    + @id
                    + " was not found at "
                    + DateTime.Now.ToShortTimeString();

                Logger.WriteGetItemNotFoundLog(@logData);

                throw new Exception(@applicationRole.GetType().Name
                    + " with Id "
                    + @id
                    + " does not exist");
            }

            return @applicationRole;
        }
    }
}
