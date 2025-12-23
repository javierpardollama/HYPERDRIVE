using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class AddDriveItemHandler : IRequestHandler<AddDriveItemCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _driveItemManager;
    private readonly IDriveItemInfoManager _driveItemInfoManager;
    private readonly IDriveItemContentManager _driveItemContentManager;
    private readonly IApplicationUserManager _applicationUserManager;

    public AddDriveItemHandler(IDriveItemManager driveItemManager,
        IDriveItemInfoManager driveItemInfoManager,
        IDriveItemContentManager driveItemContentManager,
        IApplicationUserManager applicationUserManager)
    {
        _driveItemManager = driveItemManager;
        _driveItemInfoManager = driveItemInfoManager;
        _driveItemContentManager = driveItemContentManager;
        _applicationUserManager = applicationUserManager;
    }
    
    public async Task<ViewDriveItem> Handle(AddDriveItemCommand request, CancellationToken cancellationToken)
    {
        var @by = await _applicationUserManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);

        await _driveItemManager.CheckFileName(request.ViewModel.FileName,
                                              request.ViewModel.ParentId,
                                              @by.Id);

        var @archive = await _driveItemManager.AddDriveItem(request.ViewModel.FileName,
                                                            request.ViewModel.ParentId,
                                                            request.ViewModel.Folder,
                                                            @by.Id);
       

        var @version = await _driveItemInfoManager.AddAsFileNameInfo(@archive.Id,request.ViewModel.FileName);

        await _driveItemContentManager.AddAsFileContent(@version.Id, request.ViewModel.Type, request.ViewModel.Size, request.ViewModel.Data);

        var @dto = await _driveItemManager.ReloadDriveItemById(@archive.Id);

        return @dto.ToViewModel();
    }
}