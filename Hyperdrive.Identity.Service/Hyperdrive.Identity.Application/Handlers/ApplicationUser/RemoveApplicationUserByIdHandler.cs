using Hyperdrive.Identity.Application.Commands.ApplicationUser;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationUser;

public class RemoveApplicationUserByIdHandler : IRequestHandler<RemoveApplicationUserByIdCommand>
{
    private readonly IApplicationUserManager _manager;

    public RemoveApplicationUserByIdHandler(IApplicationUserManager manager)
    {
        _manager = manager;
    }

    public async Task Handle(RemoveApplicationUserByIdCommand request, CancellationToken cancellationToken)
    {
        var @user = await _manager.FindApplicationUserById(request.Id);

        await _manager.RemoveApplicationUser(@user);
    }
}