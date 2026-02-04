using Hyperdrive.Main.Application.Commands.ApplicationRole;
using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Main.Domain.Entities;

namespace Hyperdrive.Main.Application.Handlers.ApplicationRole;

public class UpdateApplicationRoleHandler : IRequestHandler<UpdateApplicationRoleCommand, ViewApplicationRole>
{
    private readonly IApplicationRoleManager _manager;

    public UpdateApplicationRoleHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewApplicationRole> Handle(UpdateApplicationRoleCommand request, CancellationToken cancellationToken)
    {
        Entities.ApplicationRole entity = new()
        {
            Id = request.ViewModel.Id,
            Name = request.ViewModel.Name,
            ImageUri = request.ViewModel.ImageUri
        };

        var dto = await _manager.UpdateApplicationRole(entity);

        return dto.ToViewModel();
    }
}