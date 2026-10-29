using Hyperdrive.Storage.Application.Commands.ApplicationUser;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;
using Entities = Hyperdrive.Storage.Domain.Entities;

namespace Hyperdrive.Storage.Application.Handlers.ApplicationUser;

/// <summary>
/// Represents a <see cref="UpdateApplicationUserHandler"/>. Implements <see cref="IRequestHandler{UpdateApplicationUserCommand, ViewCatalog}"/>
/// </summary>
public class UpdateApplicationUserHandler : IRequestHandler<UpdateApplicationUserCommand, ViewCatalog>
{
    private readonly IApplicationUserManager Manager;

    /// <summary>
    ///  Initializes a new Instance of <see cref="UpdateApplicationUserHandler" />
    /// </summary>
    /// <param name="manager">Injected <see cref="IApplicationUserManager"/></param>
    public UpdateApplicationUserHandler(IApplicationUserManager manager)
    {
        Manager = manager;
    }

    /// <summary>
    /// Handles Request
    /// </summary>
    /// <param name="request">Injected <see cref="AddApplicationUserCommand"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task{ViewCatalog}"/></returns>
    public async Task<ViewCatalog> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var @user = new Entities.ApplicationUser()
        {
            Id = request.ViewModel.Id,
            FirstName =  request.ViewModel.FirstName,
            LastName =  request.ViewModel.LastName,
        };

        var @entity = await Manager.UpdateApplicationUser(@user);

        var @dto = await Manager.ReloadApplicationUserById(@entity.Id);

        return @dto.ToViewModel();
    }
}