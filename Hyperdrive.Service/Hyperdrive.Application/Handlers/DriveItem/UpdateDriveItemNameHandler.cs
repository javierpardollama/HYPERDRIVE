using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
        await _manager.CheckName(request.ViewModel.Name,
                                 request.ViewModel.Id,
                                 request.ViewModel.ParentId,
                                 request.ViewModel.ApplicationUserId,
                                 request.ViewModel.Extension);

        var @archive = await _manager.FindDriveItemById(request.ViewModel.Id);
        
        await _manager.AddAsNameInfo(@archive.Id,
                                         request.ViewModel.Name,
                                         request.ViewModel.Extension);      

        var @dto = await _manager.ReloadDriveItemById(request.ViewModel.Id);

        return @dto.ToViewModel();
    }
}