using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Queries.DriveItem;

public class FindDriveItemBinaryByIdQuery : IRequest<ViewDriveItemBinary>
{
    public int Id { get; set; }
}