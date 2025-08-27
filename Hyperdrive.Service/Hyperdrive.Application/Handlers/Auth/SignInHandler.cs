using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Auth;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.Auth;

public class SignInHandler : IRequestHandler<SignInCommand, ViewApplicationUser>
{
    public Task<ViewApplicationUser> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}