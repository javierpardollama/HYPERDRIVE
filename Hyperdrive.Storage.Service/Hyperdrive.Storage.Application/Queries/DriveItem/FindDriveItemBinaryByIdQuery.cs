using Hyperdrive.Storage.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Storage.Application.Queries.DriveItem;

public class FindDriveItemBinaryByIdQuery : IRequest<ViewDriveItemBinary>
{
    public Guid Id { get; set; }
}