using MediatR;

namespace Hyperdrive.Identity.Application.Commands.ApplicationUser;

public class RemoveApplicationUserByIdCommand : IRequest
{
    public int Id { get; set; }
}