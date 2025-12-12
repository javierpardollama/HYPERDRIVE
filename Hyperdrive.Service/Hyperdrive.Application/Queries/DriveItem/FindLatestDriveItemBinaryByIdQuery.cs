using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.DriveItem;

public class FindLatestDriveItemBinaryByIdQuery : IRequest<ViewDriveItemBinary>
{
    public int Id { get; set; }
}