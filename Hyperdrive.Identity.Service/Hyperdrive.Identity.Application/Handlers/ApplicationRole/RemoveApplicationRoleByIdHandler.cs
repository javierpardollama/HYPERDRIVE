using Hyperdrive.Identity.Application.Commands.ApplicationRole;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationRole;

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