using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Queries.DriveItem;

public class FindLatestDriveItemBinaryByIdQuery : IRequest<ViewDriveItemBinary>
{
    public Guid Id { get; set; }
}