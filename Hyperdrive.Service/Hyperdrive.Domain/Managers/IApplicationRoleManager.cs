using System.Collections.Generic;
using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IApplicationRoleManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IApplicationRoleManager : IBaseManager
    {
        /// <summary>
        /// Finds All Application Role
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{CatalogDto}}"/></returns>
        Task<ICollection<CatalogDto>> FindAllApplicationRole();

        /// <summary>
        /// Finds Paginated Application Role
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ViewPage{ApplicationRoleDto}}"/></returns>
        public Task<PageDto<ApplicationRoleDto>> FindPaginatedApplicationRole(int @index, int @size);

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
        /// <param name="entity">Injected <see cref="ApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRoleDto}"/></returns>
        Task<ApplicationRoleDto> UpdateApplicationRole(ApplicationRole @entity);

        /// <summary>
        /// Adds Application Role
        /// </summary>
        /// <param name="entity">Injected <see cref="ApplicationRole"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationRoleDto}"/></returns>
        Task<ApplicationRoleDto> AddApplicationRole(ApplicationRole @entity);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        Task<bool> CheckName(string @name);

        /// <summary>
        /// Checks Name
        /// </summary>
        /// <param name="name">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        Task<bool> CheckName(string @name, int @id);
    }
}
