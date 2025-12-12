using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class FindPaginatedDriveItemVersionByDriveItemIdHandler : IRequestHandler<FindPaginatedDriveItemVersionByDriveItemIdQuery, ViewPage<ViewDriveItemVersion>>
{
    private readonly IDriveItemVersionManager _manager;

    public FindPaginatedDriveItemVersionByDriveItemIdHandler(IDriveItemVersionManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewPage<ViewDriveItemVersion>> Handle(FindPaginatedDriveItemVersionByDriveItemIdQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedDriveItemVersionByDriveItemId(request.ViewModel.Index, request.ViewModel.Size, request.ViewModel.Id);

        return @page.ToPageViewModel();
    }
}