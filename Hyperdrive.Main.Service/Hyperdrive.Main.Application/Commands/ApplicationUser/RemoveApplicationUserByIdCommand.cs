using MediatR;

namespace Hyperdrive.Main.Application.Commands.ApplicationUser;

public class RemoveApplicationUserByIdCommand : IRequest
{
    public int Id { get; set; }
}