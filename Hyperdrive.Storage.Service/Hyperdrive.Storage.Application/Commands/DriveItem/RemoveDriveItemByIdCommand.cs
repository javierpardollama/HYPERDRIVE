using MediatR;

namespace Hyperdrive.Storage.Application.Commands.DriveItem;

public class RemoveDriveItemByIdCommand : IRequest
{
    public Guid Id { get; set; }
}