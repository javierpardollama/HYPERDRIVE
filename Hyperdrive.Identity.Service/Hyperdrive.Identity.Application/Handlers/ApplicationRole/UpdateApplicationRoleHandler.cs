using Hyperdrive.Identity.Application.Commands.ApplicationRole;
using Hyperdrive.Identity.Application.Profiles;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Hyperdrive.Identity.Domain.Managers;
using MediatR;

namespace Hyperdrive.Identity.Application.Handlers.ApplicationRole;

public class UpdateApplicationRoleHandler : IRequestHandler<UpdateApplicationRoleCommand, ViewApplicationRole>
{
    private readonly IApplicationRoleManager _manager;

    public UpdateApplicationRoleHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewApplicationRole> Handle(UpdateApplicationRoleCommand request, CancellationToken cancellationToken)
    {
        Identity.Domain.Entities.ApplicationRole entity = new()
        {
            Id = request.ViewModel.Id,
            Name = request.ViewModel.Name,
            ImageUri = request.ViewModel.ImageUri
        };

        var dto = await _manager.UpdateApplicationRole(entity);

        return dto.ToViewModel();
    }
}