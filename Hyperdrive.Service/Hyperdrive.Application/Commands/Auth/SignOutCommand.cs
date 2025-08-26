using Hyperdrive.Application.ViewModels.Auth;
using MediatR;

namespace Hyperdrive.Application.Commands.Auth;

public class SignOutCommand : IRequest
{
    public AuthSignOut ViewModel { get; set; }
}