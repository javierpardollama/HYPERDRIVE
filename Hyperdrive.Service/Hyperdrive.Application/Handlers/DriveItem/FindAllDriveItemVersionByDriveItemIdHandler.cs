using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class FindAllDriveItemVersionByDriveItemIdHandler : IRequestHandler<FindAllDriveItemVersionByDriveItemIdQuery, IList<ViewDriveItemVersion>>
{
    private readonly IDriveItemManager _manager;

    public FindAllDriveItemVersionByDriveItemIdHandler(IDriveItemManager manager)
    {
        _manager = manager;
    }

    public async Task<IList<ViewDriveItemVersion>> Handle(FindAllDriveItemVersionByDriveItemIdQuery request, CancellationToken cancellationToken)
    {
        var @items = await _manager.FindAllDriveItemVersionByDriveItemId(request.Id);

        return [.. @items.Select(x => new ViewDriveItemVersion())];
    }
}