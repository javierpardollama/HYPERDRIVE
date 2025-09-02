using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class AddDriveItemHandler : IRequestHandler<AddDriveItemCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _manager;

    public AddDriveItemHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }
    
    public Task<ViewDriveItem> Handle(AddDriveItemCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}