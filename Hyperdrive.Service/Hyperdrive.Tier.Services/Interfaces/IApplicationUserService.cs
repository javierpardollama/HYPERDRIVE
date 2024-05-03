using System.Collections.Generic;
using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.Services.Interfaces
{
    /// <summary>
    /// Represents a <see cref="IApplicationUserService"/> interface. Inherits <see cref="IBaseService"/>
    /// </summary>
    public interface IApplicationUserService : IBaseService
    {
        /// <summary>
        /// Finds All Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{ViewApplicationUser}}"/></returns>
        Task<ICollection<ViewApplicationUser>> FindAllApplicationUser();

        /// <summary>
        /// Finds Paginated Application User
        /// </summary>
        /// <param name="viewModel">Injected <see cref="FilterPageApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{ViewPage{ViewApplicationUser}}"/></returns>
        public Task<ViewPage<ViewApplicationUser>> FindPaginatedApplicationUser(FilterPageApplicationUser @viewModel);

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        Task<ApplicationUser> FindApplicationUserById(int @id);

        /// <summary>
        /// Removes Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task RemoveApplicationUserById(int @id);

        /// <summary>
        /// Updates Application User
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        Task<ViewApplicationUser> UpdateApplicationUser(UpdateApplicationUser @viewModel);

        /// <summary>
        /// Updates Application User Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationUser"/></param>
        /// <param name="entity">Injected <see cref="ApplicationUser"/></param>
        void UpdateApplicationUserRole(UpdateApplicationUser @viewModel, ApplicationUser @entity);

        /// <summary>
        /// Finds Application Role By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        Task<ApplicationRole> FindApplicationRoleById(int @id);
    }
}
