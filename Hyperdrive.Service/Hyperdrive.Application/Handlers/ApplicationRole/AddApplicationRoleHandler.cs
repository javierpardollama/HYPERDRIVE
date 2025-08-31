using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationRole;
using Hyperdrive.Application.Profiles;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Managers;
using MediatR;
using Entities = Hyperdrive.Domain.Entities;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

public class AddApplicationRoleHandler : IRequestHandler<AddApplicationRoleCommand, ViewApplicationRole>
{
    private readonly IApplicationRoleManager _manager;

    public AddApplicationRoleHandler(IApplicationRoleManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewApplicationRole> Handle(AddApplicationRoleCommand request, CancellationToken cancellationToken)
    {
        var @entity = new Entities.ApplicationRole()
        {
            Name = request.ViewModel.Name,
            ImageUri = request.ViewModel.ImageUri
        };

        var @dto = await _manager.AddApplicationRole(@entity);

        return @dto.ToViewModel();
    }
}