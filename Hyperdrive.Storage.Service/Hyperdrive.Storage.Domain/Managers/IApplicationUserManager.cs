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
    }
}
