using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="ISecurityManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface ISecurityManager : IBaseManager
    {
        /// <summary>
        /// Resets Password
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <param name="newPassword">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> ResetPassword(ApplicationUser @user, string @newPassword);

        /// <summary>
        /// Changes Password
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <param name="currentPassword">Injected <see cref="string"/></param>
        /// <param name="newPassword">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> ChangePassword(ApplicationUser @user, string @currentPassword, string @newPassword);

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> ChangeEmail(ApplicationUser @user, string @email);

        /// <summary>
        /// Changes Phone Number
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <param name="phoneNumber">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> ChangePhoneNumber(ApplicationUser @user, string @phoneNumber);

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <param name="firstName">Injected <see cref="string"/></param>
        /// <param name="lastName">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> ChangeName(ApplicationUser @user, string @firstName, string @lastName);
    }
}
