using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class UpdateDriveItemHandler : IRequestHandler<UpdateDriveItemCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _manager;

    public UpdateDriveItemHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }
    
    public Task<ViewDriveItem> Handle(UpdateDriveItemCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}