using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationRole;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

public class RemoveApplicationRoleByIdHandler : IRequestHandler<RemoveApplicationRoleByIdCommand>
{
    private readonly IApplicationRoleManager _manager;

    public RemoveApplicationRoleByIdHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task Handle(RemoveApplicationRoleByIdCommand request, CancellationToken cancellationToken)
    {
        await _manager.RemoveApplicationRoleById(request.Id);
    }
}