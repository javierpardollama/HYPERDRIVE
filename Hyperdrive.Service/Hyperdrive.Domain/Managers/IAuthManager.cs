using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;

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
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="password">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        Task<bool> SignIn(ApplicationUser @user, string email, string password);
        
        /// <summary>
        /// Signs In
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        Task SignOut(string email);

        /// <summary>
        /// Joins In
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="password">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        Task<bool> JoinIn(string email, string password);
    }
}
