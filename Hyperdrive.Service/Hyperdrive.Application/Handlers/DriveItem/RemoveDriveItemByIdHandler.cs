using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class RemoveDriveItemByIdHandler : IRequestHandler<RemoveDriveItemByIdCommand>
{
    public Task Handle(RemoveDriveItemByIdCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}