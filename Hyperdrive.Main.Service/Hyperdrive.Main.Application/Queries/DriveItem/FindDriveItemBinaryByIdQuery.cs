using Hyperdrive.Main.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Main.Application.Queries.DriveItem;

public class FindDriveItemBinaryByIdQuery : IRequest<ViewDriveItemBinary>
{
    public int Id { get; set; }
}