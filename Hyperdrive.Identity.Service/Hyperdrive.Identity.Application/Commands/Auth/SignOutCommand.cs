using Hyperdrive.Identity.Application.ViewModels.Auth;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.Auth;

public class SignOutCommand : IRequest
{
    public AuthSignOut ViewModel { get; set; }
}