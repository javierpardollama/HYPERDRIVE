using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.DriveItem;

public class FindLatestDriveItemBinaryByIdHandler : IRequestHandler<FindLatestDriveItemBinaryByIdQuery, ViewDriveItemBinary>
{
    private readonly IDriveItemBinaryManager _manager;

    public FindLatestDriveItemBinaryByIdHandler(IDriveItemBinaryManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewDriveItemBinary> Handle(FindLatestDriveItemBinaryByIdQuery request, CancellationToken cancellationToken)
    {
        var @item = await _manager.FindLatestDriveItemBinaryById(request.Id);

        return @item.ToViewModel();
    }
}