using Hyperdrive.Storage.Domain.Dtos;
using Hyperdrive.Storage.Domain.Entities;

namespace Hyperdrive.Storage.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IApplicationUserManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IApplicationUserManager : IBaseManager
    {
        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public Task<ApplicationUser> FindApplicationUserById(int @id);

        /// <summary>
        /// Finds All Application User By Ids
        /// </summary>
        /// <param name="ids">Injected <see cref="ICollection{int}"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public Task<IList<ApplicationUser>> FindAllApplicationUserByIds(ICollection<int> @ids);
        
        /// <summary>
        ///     Removes ApplicationUser By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int" /></param>
        /// <returns>Instance of <see cref="Task" /></returns>
        public Task RemoveApplicationUserById(int @id);

        /// <summary>
        ///     Updates ApplicationUser
        /// </summary>
        /// <param name="entity">Injected <see cref="ApplicationUser" /></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}" /></returns>
        public Task<ApplicationUser> UpdateApplicationUser(ApplicationUser @entity);

        /// <summary>
        ///     Adds ApplicationUser
        /// </summary>
        /// <param name="entity">Injected <see cref="ApplicationUser" /></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}" /></returns>
        public Task<ApplicationUser> AddApplicationUser(ApplicationUser @entity);
        
        /// <summary>
        ///     Reloads ApplicationUser By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int" /></param>
        /// <returns>Instance of <see cref="Task{CatalogDto}" /></returns>
        public Task<CatalogDto> ReloadApplicationUserById(int @id);
    }
}
