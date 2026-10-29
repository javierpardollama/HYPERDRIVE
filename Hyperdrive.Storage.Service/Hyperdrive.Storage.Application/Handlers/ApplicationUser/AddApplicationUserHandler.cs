using Hyperdrive.Storage.Application.Commands.ApplicationUser;
using Hyperdrive.Storage.Application.Profiles;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Hyperdrive.Storage.Domain.Managers;
using MediatR;
using Entities = Hyperdrive.Storage.Domain.Entities;

namespace Hyperdrive.Storage.Application.Handlers.ApplicationUser;

/// <summary>
/// Represents a <see cref="AddApplicationUserHandler"/>. Implements <see cref="IRequestHandler{AddApplicationUserCommand, ViewCatalog}"/>
/// </summary>
public class AddApplicationUserHandler : IRequestHandler<AddApplicationUserCommand, ViewCatalog>
{
    private readonly IApplicationUserManager Manager;

    /// <summary>
    ///  Initializes a new Instance of <see cref="AddApplicationUserHandler" />
    /// </summary>
    /// <param name="manager">Injected <see cref="IApplicationUserManager"/></param>
    public AddApplicationUserHandler(IApplicationUserManager manager)
    {
        Manager = manager;
    }

    /// <summary>
    /// Handles Request
    /// </summary>
    /// <param name="request">Injected <see cref="AddApplicationUserCommand"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task{ViewCatalog}"/></returns>
    public async Task<ViewCatalog> Handle(AddApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var @user = new Entities.ApplicationUser()
        {
           Id = request.ViewModel.Id,
           FirstName =  request.ViewModel.FirstName,
           LastName =  request.ViewModel.LastName,
        };

        var @entity = await Manager.AddApplicationUser(@user);

        var @dto = await Manager.ReloadApplicationUserById(@entity.Id);

        return @dto.ToViewModel();
    }
}