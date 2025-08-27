using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.Auth;
using MediatR;

namespace Hyperdrive.Application.Handlers.Auth;

public class SignOutHandler : IRequestHandler<SignOutCommand>
{
    public Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}