using MediatR;

namespace Hyperdrive.Application.Commands.ApplicationUser;

public class RemoveApplicationUserByIdCommand : IRequest
{
    public int Id { get; set; }
}