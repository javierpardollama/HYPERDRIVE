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
    private readonly IDriveItemManager _driveItemManager;
    private readonly IApplicationUserManager _applicationUserManager;

    public UpdateDriveItemHandler(IDriveItemManager driveItemManager, IApplicationUserManager applicationUserManager)
    {
        _driveItemManager = driveItemManager;
        _applicationUserManager = applicationUserManager;
    }

    public async Task<ViewDriveItem> Handle(UpdateDriveItemCommand request, CancellationToken cancellationToken)
    {
        var @by = await _applicationUserManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);
        var @archive = await _driveItemManager.FindDriveItemByFileName(request.ViewModel.FileName, request.ViewModel.ParentId, by);
               
        await _driveItemManager.AddActivity(@archive,
            request.ViewModel.Type, 
            request.ViewModel.Size, 
            request.ViewModel.Data);
               
        var @dto = await _driveItemManager.ReloadDriveItemById(@archive.Id);

        return @dto.ToViewModel();

    }
}