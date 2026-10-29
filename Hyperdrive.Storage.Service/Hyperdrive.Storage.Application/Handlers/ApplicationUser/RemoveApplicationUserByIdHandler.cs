using Hyperdrive.Storage.Application.Commands.ApplicationUser;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;

namespace Hyperdrive.Storage.Application.Handlers.ApplicationUser;

/// <summary>
/// Represents a <see cref="RemoveApplicationUserByIdHandler"/>. Implements <see cref="IRequestHandler{RemoveApplicationUserByIdCommand}"/>
/// </summary>
public class RemoveApplicationUserByIdHandler : IRequestHandler<RemoveApplicationUserByIdCommand>
{
    private readonly IApplicationUserManager Manager;

    /// <summary>
    ///  Initializes a new Instance of <see cref="RemoveApplicationUserByIdHandler" />
    /// </summary>
    /// <param name="manager">Injected <see cref="IApplicationUserManager"/></param>
    public RemoveApplicationUserByIdHandler(IApplicationUserManager manager)
    {
        Manager = manager;
    }

    /// <summary>
    /// Handles Request
    /// </summary>
    /// <param name="request">Injected <see cref="RemoveApplicationUserByIdCommand"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Handle(RemoveApplicationUserByIdCommand request, CancellationToken cancellationToken)
    {
        await Manager.RemoveApplicationUserById(request.Id);
    }
}