using Hyperdrive.Storage.Application.Commands.DriveItem;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

public class RemoveDriveItemByIdHandler : IRequestHandler<RemoveDriveItemByIdCommand>
{
    private readonly IDriveItemManager _manager;

    public RemoveDriveItemByIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }

    public async Task Handle(RemoveDriveItemByIdCommand request, CancellationToken cancellationToken)
    {
        var @archive = await _manager.FindDriveItemById(request.Id);

        await _manager.RemoveDriveItem(@archive);
    }
}