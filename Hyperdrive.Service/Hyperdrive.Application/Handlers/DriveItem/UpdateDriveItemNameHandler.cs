using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Application.Profiles;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class UpdateDriveItemNameHandler : IRequestHandler<UpdateDriveItemNameCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _manager;

    public UpdateDriveItemNameHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }
    
    public async Task<ViewDriveItem> Handle(UpdateDriveItemNameCommand request, CancellationToken cancellationToken)
    {
        await _manager.ChangeName(request.ViewModel.Name,request.ViewModel.Extension, request.ViewModel.Id, request.ViewModel.ParentId );
        
        var @dto = await _manager.ReloadDriveItemById(request.ViewModel.Id);

        return @dto.ToViewModel();
    }
}