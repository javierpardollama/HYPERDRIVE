using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.DriveItem;

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