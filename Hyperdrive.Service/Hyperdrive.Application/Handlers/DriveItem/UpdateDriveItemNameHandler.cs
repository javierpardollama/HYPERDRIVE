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
    private readonly IDriveItemManager _driveItemManager;
    private readonly IDriveItemInfoManager _driveItemInfoManager;

    public UpdateDriveItemNameHandler(IDriveItemManager driveItemManager, IDriveItemInfoManager driveItemInfoManager)
    {
        _driveItemManager = driveItemManager;
        _driveItemInfoManager = driveItemInfoManager;
    }
    
    public async Task<ViewDriveItem> Handle(UpdateDriveItemNameCommand request, CancellationToken cancellationToken)
    {
        await _driveItemManager.CheckName(request.ViewModel.Name,
                                 request.ViewModel.Id,
                                 request.ViewModel.ParentId,
                                 request.ViewModel.ApplicationUserId,
                                 request.ViewModel.Extension);

        var @archive = await _driveItemManager.FindDriveItemById(request.ViewModel.Id);
        
        await _driveItemInfoManager.AddAsNameInfo(@archive.Id,
                                         request.ViewModel.Name,
                                         request.ViewModel.Extension);      

        var @dto = await _driveItemManager.ReloadDriveItemById(request.ViewModel.Id);

        return @dto.ToViewModel();
    }
}