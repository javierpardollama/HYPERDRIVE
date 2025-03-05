using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Interfaces
{
    /// <summary>
    /// Represents a <see cref="IApplicationRoleService"/> interface. Inherits <see cref="IBaseService"/>
    /// </summary>
    public interface IApplicationRoleService : IBaseService
    {
        /// <summary>
        /// Finds All Application Role
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{ViewApplicationRole}}"/></returns>
        Task<ICollection<ViewApplicationRole>> FindAllApplicationRole();

        /// <summary>
        /// Finds Paginated Application Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="FilterPageApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ViewPage{ViewApplicationRole}}"/></returns>
        public Task<ViewPage<ViewApplicationRole>> FindPaginatedApplicationRole(FilterPageApplicationRole @viewModel);

        /// <summary>
        /// Finds Application Role By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        Task<ApplicationRole> FindApplicationRoleById(int @id);

        /// <summary>
        /// Removes Application Role By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task RemoveApplicationRoleById(int @id);

        /// <summary>
        /// Updates Application Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationRole}"/></returns>
        Task<ViewApplicationRole> UpdateApplicationRole(UpdateApplicationRole @viewModel);

        /// <summary>
        /// Adds Application Role
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationRole}"/></returns>
        Task<ViewApplicationRole> AddApplicationRole(AddApplicationRole @viewModel);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="AddApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        Task<ApplicationRole> CheckName(AddApplicationRole @viewModel);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="UpdateApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRole}"/></returns>
        Task<ApplicationRole> CheckName(UpdateApplicationRole @viewModel);
    }
}
