using Hyperdrive.Main.Application.ViewModels.Security;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.Security;

public class RefreshTokenResetCommand : IRequest<ViewApplicationUser>
{
    public SecurityRefreshTokenReset ViewModel { get; set; }
}