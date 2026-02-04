using Hyperdrive.Main.Application.Commands.ApplicationRole;
using Hyperdrive.Main.Application.Profiles;
using Hyperdrive.Main.Application.ViewModels.Views;
using Hyperdrive.Main.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Main.Domain.Entities;

namespace Hyperdrive.Main.Application.Handlers.ApplicationRole;

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