using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Application.Profiles;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class UpdateDriveItemSharedWithHandler : IRequestHandler<UpdateDriveItemSharedWithCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _driveItemManager;
    private readonly IApplicationUserManager _applicationUserManager;

    public UpdateDriveItemSharedWithHandler(IDriveItemManager driveItemManager, IApplicationUserManager applicationUserManager)
    {
        _driveItemManager = driveItemManager;
        _applicationUserManager = applicationUserManager;
    }
    
    public async Task<ViewDriveItem> Handle(UpdateDriveItemSharedWithCommand request, CancellationToken cancellationToken)
    {
        var @archive = await _driveItemManager.FindDriveItemById(request.ViewModel.Id);
        
        var @users = await _applicationUserManager.FindAllApplicationUserByIds(request.ViewModel.ApplicationUserIds);
        
        await _driveItemManager.AddSharedWith(users, archive);
        
        var @dto = await _driveItemManager.ReloadDriveItemById(@archive.Id);

        return @dto.ToViewModel();
    }
}