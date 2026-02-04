using Hyperdrive.Main.Application.ViewModels.Auth;
using MediatR;

namespace Hyperdrive.Main.Application.Commands.Auth;

public class SignOutCommand : IRequest
{
    public AuthSignOut ViewModel { get; set; }
}