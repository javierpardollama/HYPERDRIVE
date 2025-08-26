using Hyperdrive.Application.ViewModels.Auth;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Commands.Auth;

public class SignInCommand : IRequest<ViewApplicationUser>
{
    public AuthSignIn ViewModel { get; set; }
}