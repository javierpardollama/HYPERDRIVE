using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.DriveItem;

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