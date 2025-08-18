using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;

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
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="newPassword">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> ResetPassword(int @id, string @newPassword);

        /// <summary>
        /// Changes Password
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="currentPassword">Injected <see cref="string"/></param>
        /// <param name="newPassword">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> ChangePassword(int @id, string @currentPassword, string @newPassword);

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <param name="id">Injected <see cref="string"/></param>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> ChangeEmail(int id, string @email);

        /// <summary>
        /// Changes Phone Number
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="phoneNumber">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> ChangePhoneNumber(int @id, string @phoneNumber);

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="firstName">Injected <see cref="string"/></param>
        /// <param name="lastName">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> ChangeName(int @id, string @firstName, string @lastName);

        /// <summary>
        /// Refreshes Tokens
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <param name="token">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        Task<ApplicationUserDto> RefreshTokens(int @id, string @token);
    }
}
