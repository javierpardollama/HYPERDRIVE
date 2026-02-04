using Hyperdrive.Main.Application.ViewModels.Auth;
using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.Auth;

public class JoinInCommand : IRequest<ViewApplicationUser>
{
    public AuthJoinIn ViewModel { get; set; }
}