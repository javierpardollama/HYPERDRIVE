using Hyperdrive.Application.ViewModels.Auth;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.Auth;

public class JoinInCommand : IRequest<ViewApplicationUser>
{
    public AuthJoinIn ViewModel { get; set; }
}