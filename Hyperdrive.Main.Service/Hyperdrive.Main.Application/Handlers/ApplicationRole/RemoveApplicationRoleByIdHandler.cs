using Hyperdrive.Main.Application.Commands.ApplicationRole;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.ApplicationRole;

public class RemoveApplicationRoleByIdHandler : IRequestHandler<RemoveApplicationRoleByIdCommand>
{
    private readonly IApplicationRoleManager _manager;

    public RemoveApplicationRoleByIdHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task Handle(RemoveApplicationRoleByIdCommand request, CancellationToken cancellationToken)
    {
        var @role = await _manager.FindApplicationRoleById(request.Id);

        await _manager.RemoveApplicationRole(@role);
    }
}