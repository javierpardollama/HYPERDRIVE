using Hyperdrive.Storage.Application.Commands.DriveItem;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

public class UpdateDriveItemSharedWithHandler : IRequestHandler<UpdateDriveItemSharedWithCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _driveItemManager;

    public UpdateDriveItemSharedWithHandler(IDriveItemManager driveItemManager)
    {
        _driveItemManager = driveItemManager;
    }

    public async Task<ViewDriveItem> Handle(UpdateDriveItemSharedWithCommand request, CancellationToken cancellationToken)
    {
        var @archive = await _driveItemManager.FindDriveItemById(request.ViewModel.Id);

        await _driveItemManager.AddSharedWith(request.ViewModel.ApplicationUserIds, archive);

        var @dto = await _driveItemManager.ReloadDriveItemById(@archive.Id);

        return @dto.ToViewModel();
    }
}