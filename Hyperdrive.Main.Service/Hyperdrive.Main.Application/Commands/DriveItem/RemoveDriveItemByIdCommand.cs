using MediatR;

namespace Hyperdrive.Main.Application.Commands.DriveItem;

public class RemoveDriveItemByIdCommand : IRequest
{
    public int Id { get; set; }
}