using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.Profiles;
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
    
    public async Task<ViewDriveItem> Handle(UpdateDriveItemCommand request, CancellationToken cancellationToken)
    {
        var @archive = await _manager.FindDriveItemById(request.ViewModel.Id);
        
        if (!@archive.Folder)
        {
            await _manager.AddActivity(@archive,
                request.ViewModel.Type, 
                request.ViewModel.Size, 
                request.ViewModel.Data);
        }
        
        var @dto = await _manager.ReloadDriveItemById(@archive.Id);

        return @dto.ToViewModel();

    }
}