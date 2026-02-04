using Hyperdrive.Main.Application.Commands.DriveItem;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.DriveItem;

public class TargetDriveItemVersionByIdHandler : IRequestHandler<TargetDriveItemVersionByIdCommand>
{
    private readonly IDriveItemVersionManager _manager;

    public TargetDriveItemVersionByIdHandler(IDriveItemVersionManager manager)
    {
        _manager = manager;
    }

    public async Task Handle(TargetDriveItemVersionByIdCommand request, CancellationToken cancellationToken)
    {
        var @version = await _manager.FindDriveItemVersionById(request.Id);

        await _manager.TargetDriveItemVersion(@version);
    }
}