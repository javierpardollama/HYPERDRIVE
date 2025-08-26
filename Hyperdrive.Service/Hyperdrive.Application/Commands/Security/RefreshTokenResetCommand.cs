using Hyperdrive.Application.ViewModels.Security;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.Security;

public class RefreshTokenResetCommand : IRequest<ViewApplicationUser>
{
    public SecurityRefreshTokenReset ViewModel { get; set; }
}