using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Security;
using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.Services.Interfaces
{
    public interface ISecurityService : IBaseService
    {
        Task<ApplicationUser> FindApplicationUserByEmail(string email);

        Task<ViewApplicationUser> ResetPassword(SecurityPasswordReset viewModel);

        Task<ViewApplicationUser> ChangePassword(SecurityPasswordChange viewModel);

        Task<ViewApplicationUser> ChangeEmail(SecurityEmailChange viewModel);
    }
}
