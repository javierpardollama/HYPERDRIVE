using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class RemoveDriveItemByIdHandler : IRequestHandler<RemoveDriveItemByIdCommand>
{
    private readonly IDriveItemManager _manager;

    public RemoveDriveItemByIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }
    
    public async Task Handle(RemoveDriveItemByIdCommand request, CancellationToken cancellationToken)
    {
        await _manager.RemoveDriveItemById(request.Id);
    }
}