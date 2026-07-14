using Hyperdrive.Storage.Application.Commands.DriveItem;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

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