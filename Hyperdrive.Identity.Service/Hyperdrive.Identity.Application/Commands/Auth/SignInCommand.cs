using Hyperdrive.Identity.Application.ViewModels.Auth;
using Hyperdrive.Identity.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Identity.Application.Commands.Auth;

public class SignInCommand : IRequest<ViewApplicationUser>
{
    public AuthSignIn ViewModel { get; set; }
}