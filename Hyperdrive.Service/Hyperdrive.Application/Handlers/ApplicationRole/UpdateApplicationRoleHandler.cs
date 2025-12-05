using Hyperdrive.Application.Commands.ApplicationRole;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Domain.Entities;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

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