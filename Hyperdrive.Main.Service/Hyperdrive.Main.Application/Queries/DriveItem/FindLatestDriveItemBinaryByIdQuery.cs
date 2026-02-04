using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.DriveItem;

public class FindLatestDriveItemBinaryByIdQuery : IRequest<ViewDriveItemBinary>
{
    public int Id { get; set; }
}