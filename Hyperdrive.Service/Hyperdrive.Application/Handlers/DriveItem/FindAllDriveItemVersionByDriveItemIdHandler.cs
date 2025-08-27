using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Queries.DriveItem;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.DriveItem;

public class FindAllDriveItemVersionByDriveItemIdHandler : IRequestHandler<FindAllDriveItemVersionByDriveItemIdQuery, IList<ViewDriveItemVersion>>
{
    public Task<IList<ViewDriveItemVersion>> Handle(FindAllDriveItemVersionByDriveItemIdQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}