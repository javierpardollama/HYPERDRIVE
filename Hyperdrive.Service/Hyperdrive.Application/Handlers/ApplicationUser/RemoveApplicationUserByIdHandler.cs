using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationUser;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

public class RemoveApplicationUserByIdHandler : IRequestHandler<RemoveApplicationUserByIdCommand>
{
    public Task Handle(RemoveApplicationUserByIdCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}