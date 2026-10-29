using Hyperdrive.Storage.Application.Commands.DriveItem;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

public class AddDriveItemHandler : IRequestHandler<AddDriveItemCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _driveItemManager;
    private readonly IDriveItemInfoManager _driveItemInfoManager;
    private readonly IDriveItemContentManager _driveItemContentManager;

    public AddDriveItemHandler(IDriveItemManager driveItemManager,
        IDriveItemInfoManager driveItemInfoManager,
        IDriveItemContentManager driveItemContentManager)
    {
        _driveItemManager = driveItemManager;
        _driveItemInfoManager = driveItemInfoManager;
        _driveItemContentManager = driveItemContentManager;
    }

    public async Task<ViewDriveItem> Handle(AddDriveItemCommand request, CancellationToken cancellationToken)
    {

        await _driveItemManager.CheckFileName(request.ViewModel.FileName,
                                              request.ViewModel.ParentId,
                                              request.ViewModel.ApplicationUserId);

        var @archive = await _driveItemManager.AddDriveItem(request.ViewModel.ParentId,
                                                            request.ViewModel.Folder,
                                                            request.ViewModel.ApplicationUserId);


        var @version = await _driveItemInfoManager.AddAsFileNameInfo(@archive.Id, request.ViewModel.FileName);

        await _driveItemContentManager.AddAsFileContent(@version.Id,
            request.ViewModel.Type,
            request.ViewModel.Size,
            request.ViewModel.Data,
            request.ViewModel.Folder);

        var @dto = await _driveItemManager.ReloadDriveItemById(@archive.Id);

        return @dto.ToViewModel();
    }
}