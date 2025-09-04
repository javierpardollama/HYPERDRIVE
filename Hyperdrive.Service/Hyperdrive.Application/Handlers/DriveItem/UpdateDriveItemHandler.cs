using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Application.Profiles;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class UpdateDriveItemHandler : IRequestHandler<UpdateDriveItemCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _manager;

    public UpdateDriveItemHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }
    
    public async Task<ViewDriveItem> Handle(UpdateDriveItemCommand request, CancellationToken cancellationToken)
    {
        await _manager.ChangeName(request.ViewModel.Name, request.ViewModel.Id, request.ViewModel.ParentId );
        
        var @dto = await _manager.ReloadDriveItemById(request.ViewModel.Id);

        return @dto.ToViewModel();
    }
}