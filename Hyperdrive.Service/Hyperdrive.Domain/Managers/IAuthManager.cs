using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IAuthManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IAuthManager : IBaseManager
    {
        /// <summary>
        /// Signs In
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="password">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> SignIn(string email, string password);

        /// <summary>
        /// Joins In
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="password">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> JoinIn(string email, string password);

        /// <summary>
        /// Signs Out
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        Task SignOut(int @id);
    }
}
