using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.DriveItem;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class AddDriveItemHandler : IRequestHandler<AddDriveItemCommand, ViewDriveItem>
{
    private readonly IDriveItemManager _driveItemManager;
    private readonly IApplicationUserManager _applicationUserManager;

    public AddDriveItemHandler(IDriveItemManager driveItemManager, IApplicationUserManager applicationUserManager)
    {
        _driveItemManager = driveItemManager;
        _applicationUserManager = applicationUserManager;
    }
    
    public async Task<ViewDriveItem> Handle(AddDriveItemCommand request, CancellationToken cancellationToken)
    {
        var @by = await _applicationUserManager.FindApplicationUserById(request.ViewModel.ApplicationUserId);

        await _driveItemManager.CheckFileName(request.ViewModel.FileName,
                                              request.ViewModel.ParentId,
                                              request.ViewModel.ApplicationUserId);

        var @archive = await _driveItemManager.AddDriveItem(request.ViewModel.FileName,
                                                            request.ViewModel.ParentId,
                                                            request.ViewModel.Folder,
                                                            @by);
       
        await _driveItemManager.AddAsFileNameActivity(@archive,
                                                      request.ViewModel.FileName,
                                                      request.ViewModel.Type,
                                                      request.ViewModel.Size,
                                                      request.ViewModel.Data);
       
        
        var @dto = await _driveItemManager.ReloadDriveItemById(@archive.Id);

        return @dto.ToViewModel();
    }
}