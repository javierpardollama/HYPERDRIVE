using System.Threading;
using System.Threading.Tasks;
using Hyperdrive.Application.Commands.ApplicationRole;
using Hyperdrive.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Application.Handlers.ApplicationRole;

public class AddApplicationRoleHandler : IRequestHandler<AddApplicationRoleCommand, ViewApplicationRole>
{
    public Task<ViewApplicationRole> Handle(AddApplicationRoleCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}