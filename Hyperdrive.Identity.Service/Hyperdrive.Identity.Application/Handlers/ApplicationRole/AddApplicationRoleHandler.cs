using Hyperdrive.Identity.Application.Commands.ApplicationRole;
using Hyperdrive.Identity.Application.Profiles;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationRole;

public class AddApplicationRoleHandler : IRequestHandler<AddApplicationRoleCommand, ViewApplicationRole>
{
    private readonly IApplicationRoleManager _manager;

    public AddApplicationRoleHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewApplicationRole> Handle(AddApplicationRoleCommand request, CancellationToken cancellationToken)
    {
        var @entity = new Identity.Domain.Entities.ApplicationRole()
        {
            Name = request.ViewModel.Name,
            ImageUri = request.ViewModel.ImageUri
        };

        var @dto = await _manager.AddApplicationRole(@entity);

        return @dto.ToViewModel();
    }
}