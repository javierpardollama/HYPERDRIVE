using MediatR;

namespace Hyperdrive.Application.Commands.DriveItem;

public class RemoveDriveItemByIdCommand : IRequest
{
    public int Id { get; set; }
}