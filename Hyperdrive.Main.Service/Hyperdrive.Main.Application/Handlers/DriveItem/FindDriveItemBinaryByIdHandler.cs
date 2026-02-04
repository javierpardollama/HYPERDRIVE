using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.Queries.DriveItem;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Application.Handlers.DriveItem;

public class FindDriveItemBinaryByIdHandler : IRequestHandler<FindDriveItemBinaryByIdQuery, ViewDriveItemBinary>
{
    private readonly IDriveItemBinaryManager _manager;

    public FindDriveItemBinaryByIdHandler(IDriveItemBinaryManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewDriveItemBinary> Handle(FindDriveItemBinaryByIdQuery request, CancellationToken cancellationToken)
    {
        var @item = await _manager.FindDriveItemBinaryById(request.Id);

        return @item.ToViewModel();
    }
}