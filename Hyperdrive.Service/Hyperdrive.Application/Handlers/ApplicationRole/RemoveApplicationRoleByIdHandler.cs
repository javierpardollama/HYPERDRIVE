using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationRole;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

public class RemoveApplicationRoleByIdHandler : IRequestHandler<RemoveApplicationRoleByIdCommand>
{
    public Task Handle(RemoveApplicationRoleByIdCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}