using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class UpdateDriveItemHandler : IRequestHandler<UpdateDriveItemCommand, ViewDriveItem>
{
    public Task<ViewDriveItem> Handle(UpdateDriveItemCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}