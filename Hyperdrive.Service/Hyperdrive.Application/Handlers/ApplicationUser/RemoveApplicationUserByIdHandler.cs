using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationUser;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationUser;

public class RemoveApplicationUserByIdHandler : IRequestHandler<RemoveApplicationUserByIdCommand>
{
    private readonly IApplicationUserManager _manager;

    public RemoveApplicationUserByIdHandler(IApplicationUserManager manager)
    {
        _manager = manager;
    }
    
    public async Task Handle(RemoveApplicationUserByIdCommand request, CancellationToken cancellationToken)
    {
        await _manager.RemoveApplicationUserById(request.Id);
    }
}