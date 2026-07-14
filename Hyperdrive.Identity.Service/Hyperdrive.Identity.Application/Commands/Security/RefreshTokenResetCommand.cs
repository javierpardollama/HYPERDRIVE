using Hyperdrive.Identity.Application.ViewModels.Security;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.Security;

public class RefreshTokenResetCommand : IRequest<ViewApplicationUser>
{
    public SecurityRefreshTokenReset ViewModel { get; set; }
}