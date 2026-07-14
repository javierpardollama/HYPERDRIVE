using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

public class FindPaginatedSharedDriveItemByApplicationUserIdHandler : IRequestHandler<FindPaginatedSharedDriveItemByApplicationUserIdQuery, ViewPage<ViewDriveItem>>
{
    private readonly IDriveItemManager _manager;

    public FindPaginatedSharedDriveItemByApplicationUserIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewPage<ViewDriveItem>> Handle(FindPaginatedSharedDriveItemByApplicationUserIdQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedSharedDriveItemWithApplicationUserId(
            request.ViewModel.Index,
            request.ViewModel.Size,
            request.ViewModel.ApplicationUserId
        );

        return @page.ToPageViewModel();
    }
}