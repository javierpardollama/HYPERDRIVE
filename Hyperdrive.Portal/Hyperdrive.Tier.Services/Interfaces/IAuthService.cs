using System.Threading.Tasks;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Auth;
using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.Services.Interfaces
{
    public interface IAuthService : IBaseService
    {
        Task<ViewApplicationUser> SignIn(AuthSignIn viewModel);

        Task<ViewApplicationUser> SignIn(AuthJoinIn viewModel);

        Task<ViewApplicationUser> JoinIn(AuthJoinIn viewModel);

        Task<ApplicationUser> FindApplicationUserByEmail(string email);

        Task<ApplicationUser> CheckEmail(AuthJoinIn viewModel);
    }
}
