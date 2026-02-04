using MediatR;

namespace Hyperdrive.Main.Application.Commands.DriveItem;

public class TargetDriveItemVersionByIdCommand : IRequest
{
    public int Id { get; set; }
}