using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.DriveItem;

public class FindPaginatedDriveItemByApplicationUserIdHandler : IRequestHandler<FindPaginatedDriveItemByApplicationUserIdQuery, ViewPage<ViewDriveItem>>
{
    private readonly IDriveItemManager _manager;

    public FindPaginatedDriveItemByApplicationUserIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewPage<ViewDriveItem>> Handle(FindPaginatedDriveItemByApplicationUserIdQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedDriveItemByApplicationUserId(
            request.ViewModel.Index,
            request.ViewModel.Size,
            request.ViewModel.ApplicationUserId,
            request.ViewModel.ParentId
            );

        return @page.ToPageViewModel();
    }
}