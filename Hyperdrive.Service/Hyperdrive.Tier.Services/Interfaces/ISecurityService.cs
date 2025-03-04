using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Interfaces
{
    /// <summary>
    /// Represents a <see cref="ISecurityService"/> interface. Inherits <see cref="IBaseService"/>
    /// </summary>
    public interface ISecurityService : IBaseService
    {
        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        Task<ApplicationUser> FindApplicationUserByEmail(string @email);

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        Task<ApplicationUser> FindApplicationUserById(int @id);

        /// <summary>
        /// Resets Password
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPasswordReset"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        Task<ViewApplicationUser> ResetPassword(SecurityPasswordReset @viewModel);

        /// <summary>
        /// Changes Password
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPasswordChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        Task<ViewApplicationUser> ChangePassword(SecurityPasswordChange @viewModel);

        /// <summary>
        /// Changes Email
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityEmailChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        Task<ViewApplicationUser> ChangeEmail(SecurityEmailChange @viewModel);

        /// <summary>
        /// Changes Phone Number
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityPhoneNumberChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        Task<ViewApplicationUser> ChangePhoneNumber(SecurityPhoneNumberChange @viewModel);

        /// <summary>
        /// Changes Name
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityNameChange"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        Task<ViewApplicationUser> ChangeName(SecurityNameChange @viewModel);


        /// <summary>
        /// Refreshes Tokens
        /// </summary>
        /// <param name="viewModel">Injected <see cref="SecurityRefreshTokenReset"/></param>
        /// <returns>Instance of <see cref="Task{ViewApplicationUser}"/></returns>
        Task<ViewApplicationUser> RefreshTokens(SecurityRefreshTokenReset @viewModel);
    }
}
